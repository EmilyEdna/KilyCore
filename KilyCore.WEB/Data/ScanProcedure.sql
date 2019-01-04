USE [Kily]
GO
/****** Object:  StoredProcedure [dbo].[Sp_GetScanBrandInfo]    Script Date: 2019/1/3 17:38:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		刘泽华
-- Create date: 2019年1月3日14点51分
-- Description:	一品一码手机端扫码存储过程
-- =============================================
CREATE PROCEDURE [dbo].[Sp_GetScanBrandInfo]
(
    @Id UNIQUEIDENTIFIER NULL,
    @Code NVARCHAR(MAX) NULL
)
AS
BEGIN
    DECLARE @CompanyType INT;
    DECLARE @ProductionBatchId UNIQUEIDENTIFIER;
    DECLARE @MaterList NVARCHAR(MAX);
    DECLARE @FacilityId UNIQUEIDENTIFIER;
    DECLARE @GrowId UNIQUEIDENTIFIER;
    DECLARE @GrowBatch NVARCHAR(MAX);
    -- 基本信息
    SELECT a.Id AS 二维码Id,
           b.ProductName AS 产品名称,
           b.ExpiredDate AS 产品保质期,
           c.Manager AS 入库负责人,
           c.GoodsBatchNo AS 入库批次,
           c.ImgUrl AS 产品图片,
           c.Remark AS 产品介绍,
           c.GrowNoteId AS 成长Id,
           d.OutStockUser AS 出库负责人,
           d.GoodsBatchNo AS 出库批次,
           e.CompanyName AS 生产商,
           e.CompanyAddress AS 生产地址,
           e.CompanyType AS 企业类型, -- 10种植企业,20养殖企业,30生产企业,40流通企业
           g.StartTime AS 生产时间,
           g.Id AS 生产批次Id,
           g.Manager AS 生产负责人,
           g.MaterialId AS 原料列表,
           g.DeviceName AS 设备名称,
           g.FacId AS 设施Id,
           h.BatchNo AS 进货批次,
           h.GoodName AS 进货名称,
           h.Supplier AS 供应商,
           h.CheckReport AS 检测报告,
           h.Spec AS 进货规格,
           h.Address AS 进货产地,
           i.CheckUint AS 产品质检单位,
           i.CheckUser AS 产品质检人,
           i.CheckResult AS 产品质检结果,
           i.CheckReport AS 产品质检报告
    INTO #BaseInfo
    FROM dbo.EnterpriseTagAttach AS a
        INNER JOIN dbo.EnterpriseGoods AS b
            ON a.GoodsId = b.Id
        INNER JOIN dbo.EnterpriseGoodsStock AS c
            ON c.GoodsId = b.Id
        INNER JOIN dbo.EnterpriseGoodsStockAttach AS d
            ON c.Id = d.StockId
        INNER JOIN dbo.EnterpriseInfo AS e
            ON a.CompanyId = e.Id
        INNER JOIN dbo.EnterpriseProductSeries AS f
            ON f.Id = b.ProductSeriesId
        INNER JOIN dbo.EnterpriseProductionBatch AS g
            ON g.SeriesId = f.Id
        LEFT JOIN dbo.EnterpriseBuyer AS h
            ON e.Id = h.CompanyId
        LEFT JOIN dbo.EnterpriseCheckGoods i
            ON i.Id = c.CheckGoodsId
    WHERE a.IsDelete = 0
          AND a.TagType = 2
          AND a.StarSerialNo <= @Code
          AND a.EndSerialNo >= @Code
		  AND a.StockNo=c.GoodsBatchNo;

    IF (@Id IS NOT NULL)
    BEGIN
        SELECT
               a.*
        FROM #BaseInfo AS a
        WHERE a.二维码Id = @Id;
    END;
    ELSE
    BEGIN
        SELECT
               a.*
        FROM #BaseInfo AS a;
    END;

    SELECT @CompanyType = a.企业类型,
           @ProductionBatchId = a.生产批次Id,
           @MaterList = a.原料列表,
           @FacilityId = a.设施Id,
           @GrowId = a.成长Id
    FROM #BaseInfo AS a;

    -- 查询关键点指标

    SELECT a.TargetName AS 关键点名称,
           a.TargetValue AS 关键点阙值,
           a.TargetUnit AS 关键点单位,
           a.Result AS 结果
    FROM dbo.EnterpriseProductionBatchAttach AS a
    WHERE ProBatchId = @ProductionBatchId;

    -- 查询原料&质检列表

    SELECT a.Id AS 原料Id,
           a.MaterName AS 原料名称,
           a.ExpiredDay AS 原料保质期,
           a.Supplier AS 原料供应商,
           a.BuyTime AS 采购时间,
           a.MaterCreateTime AS 原料生产时间,
           b.CheckUint AS 质检单位,
           b.CheckUser AS 质检人,
           b.CheckReport AS 质检报告,
           b.CheckResult AS 质检结果
    FROM dbo.EnterpriseMaterial AS a
        LEFT JOIN EnterpriseCheckMaterial AS b
            ON a.Id = b.MaterId
    WHERE CHARINDEX(CAST(a.Id AS NVARCHAR(MAX)), @MaterList) > 0;

    -- 查询设施信息

    SELECT a.WorkShopName AS 车间名称,
           a.Environment AS 环境信息
    FROM dbo.EnterpriseFacilities AS a
    WHERE a.Id = @FacilityId;

    -- 查询成档案

    SELECT @GrowBatch = a.BatchNo
    FROM dbo.EnterpriseNote AS a
    WHERE Id = @GrowId;

    SELECT a.LvName AS 阶段名称,
           a.LvImg AS 阶段图片
    FROM dbo.EnterpriseAgeUp AS a
    WHERE a.BatchNo = @GrowBatch;

    SELECT a.SoilReport AS 土壤报告,
           a.AirReport AS 空气报告,
           a.WaterReport AS 水质报告,
           a.MetalReport AS 金属报告
    FROM dbo.EnterpriseEnvironmentAttach AS a
    WHERE a.BatchNo = @GrowBatch;

    IF (@CompanyType = 10)
    BEGIN
        SELECT a.FeedName AS 肥料名称,
               a.PlantTime AS 施肥时间,
               a.Producter AS 肥料生产商
        FROM dbo.EnterprisePlanting AS a
        WHERE a.IsType = 1
              AND a.BatchNo = @GrowBatch;

        SELECT a.DrugName AS 药品名称,
               a.PlantTime AS 施药时间,
               a.Producter AS 农药生产商
        FROM dbo.EnterpriseDrug AS a
        WHERE a.IsType = 1
              AND a.BatchNo = @GrowBatch;
    END;
    ELSE IF(@CompanyType = 20)
    BEGIN
        SELECT a.FeedName AS 肥料名称,
               a.PlantTime AS 施肥时间,
               a.Producter AS 肥料生产商
        FROM dbo.EnterprisePlanting AS a
        WHERE a.IsType = 2
              AND a.BatchNo = @GrowBatch;

        SELECT a.DrugName AS 疫苗名称,
               a.PlantTime AS 接种时间,
               a.Producter AS 疫苗生产商
        FROM dbo.EnterpriseDrug AS a
        WHERE a.IsType = 2
              AND a.BatchNo = @GrowBatch;
    END;
    DROP TABLE #BaseInfo;
END;
GO

/****** Object:  StoredProcedure [dbo].[Sp_GetScanCodeInfo]    Script Date: 2019/1/4 10:16:10 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		刘泽华
-- Create date: 2019年1月3日16点32分
-- Description:	手机端扫码存储过程
-- =============================================
CREATE PROCEDURE [dbo].[Sp_GetScanCodeInfo]
(
    @Id UNIQUEIDENTIFIER NULL,
    @Code NVARCHAR(MAX) NULL,
	@CodeType INT NULL
)
AS
BEGIN
    DECLARE @CompanyType INT;
    DECLARE @CompanyId UNIQUEIDENTIFIER;
    DECLARE @ProductionBatchId UNIQUEIDENTIFIER;
    DECLARE @MaterList NVARCHAR(MAX);
    DECLARE @FacilityId UNIQUEIDENTIFIER;
    DECLARE @GrowId UNIQUEIDENTIFIER;
    DECLARE @GrowBatch NVARCHAR(MAX);
    DECLARE @GoodsName NVARCHAR(MAX);
    -- 基本信息
    SELECT a.Id AS 二维码Id,
           b.ProductName AS 产品名称,
           b.ExpiredDate AS 产品保质期,
           c.Manager AS 入库负责人,
           c.GoodsBatchNo AS 入库批次,
           c.ImgUrl AS 产品图片,
           c.Remark AS 产品介绍,
           c.GrowNoteId AS 成长Id,
           d.OutStockUser AS 出库负责人,
           d.GoodsBatchNo AS 出库批次,
           e.Id AS 企业Id,
           e.CompanyName AS 生产商或发布企业,
           e.CompanyAddress AS 生产地址,
           e.CompanyType AS 企业类型, -- 10种植企业,20养殖企业,30生产企业,40流通企业
           (CASE e.CompanyType
                WHEN 10 THEN
                    '种植企业'
                WHEN 20 THEN
                    '养殖企业'
                WHEN 30 THEN
                    '生产企业'
                WHEN 40 THEN
                    '流通企业'
                ELSE
                    '其他企业'
            END
           ) AS 企业类型名称,
           e.TypePath AS 所属区域,
           g.StartTime AS 生产时间,
           g.Id AS 生产批次Id,
           g.Manager AS 生产负责人,
           g.MaterialId AS 原料列表,
           g.DeviceName AS 设备名称,
           g.FacId AS 设施Id,
           h.SaveType AS 储藏方式,
           h.SaveTemp AS 储藏温度,
           h.SaveH2 AS 储藏湿度,
           i.BatchNo AS 进货批次,
           i.GoodName AS 进货名称,
           i.Supplier AS 供应商,
           i.CheckReport AS 检测报告,
           i.Spec AS 进货规格,
           i.Address AS 进货产地,
           j.CheckUint AS 产品质检单位,
           j.CheckUser AS 产品质检人,
           j.CheckResult AS 产品质检结果,
           j.CheckReport AS 产品质检报告,
           l.BatchNo AS 发货批次,
           l.SendTime AS 发货时间,
           l.GainUser AS 经销商,
           l.TransportWay AS 运输方式,
           l.Traffic AS 交通工具,
           m.IdentStar AS 认证星级
    INTO #BaseInfo
    FROM dbo.EnterpriseTagAttach AS a
        INNER JOIN dbo.EnterpriseGoods AS b
            ON a.GoodsId = b.Id
        INNER JOIN dbo.EnterpriseGoodsStock AS c
            ON c.GoodsId = b.Id
        INNER JOIN dbo.EnterpriseGoodsStockAttach AS d
            ON c.Id = d.StockId
        INNER JOIN dbo.EnterpriseInfo AS e
            ON a.CompanyId = e.Id
        INNER JOIN dbo.EnterpriseProductSeries AS f
            ON f.Id = b.ProductSeriesId
        INNER JOIN dbo.EnterpriseProductionBatch AS g
            ON g.SeriesId = f.Id
        INNER JOIN dbo.EnterpriseStockType AS h
            ON h.Id = c.StockTypeId
        LEFT JOIN dbo.EnterpriseBuyer AS i
            ON e.Id = h.CompanyId
        LEFT JOIN dbo.EnterpriseCheckGoods j
            ON i.Id = c.CheckGoodsId
        LEFT JOIN dbo.EnterpriseGoodsPackage AS k
            ON d.GoodsBatchNo = k.ProductOutStockNo
        LEFT JOIN dbo.EnterpriseLogistics AS l
            ON k.PackageNo = l.PackageNo
        LEFT JOIN dbo.EnterpriseIdent AS m
            ON e.Id = m.CompanyId
    WHERE a.IsDelete = 0
          AND a.TagType = @CodeType
          AND a.StarSerialNo <= @Code
          AND a.EndSerialNo >= @Code
          AND a.StockNo = c.GoodsBatchNo;

    IF (@Id IS NOT NULL)
    BEGIN
        SELECT a.*
        FROM #BaseInfo AS a
        WHERE a.二维码Id = @Id;
    END;
    ELSE
    BEGIN
        SELECT a.*
        FROM #BaseInfo AS a;
    END;

    SELECT @CompanyId = a.企业Id,
           @CompanyType = a.企业类型,
           @GoodsName = a.产品名称,
           @ProductionBatchId = a.生产批次Id,
           @MaterList = a.原料列表,
           @FacilityId = a.设施Id,
           @GrowId = a.成长Id
    FROM #BaseInfo AS a;

    -- 查询关键点指标

    SELECT a.TargetName AS 关键点名称,
           a.TargetValue AS 关键点阙值,
           a.TargetUnit AS 关键点单位,
           a.Result AS 结果
    FROM dbo.EnterpriseProductionBatchAttach AS a
    WHERE ProBatchId = @ProductionBatchId;

    -- 查询原料&质检列表

    SELECT a.Id AS 原料Id,
           a.MaterName AS 原料名称,
           a.ExpiredDay AS 原料保质期,
           a.Supplier AS 原料供应商,
           a.BuyTime AS 采购时间,
           a.MaterCreateTime AS 原料生产时间,
           b.CheckUint AS 质检单位,
           b.CheckUser AS 质检人,
           b.CheckReport AS 质检报告,
           b.CheckResult AS 质检结果
    FROM dbo.EnterpriseMaterial AS a
        LEFT JOIN EnterpriseCheckMaterial AS b
            ON a.Id = b.MaterId
    WHERE CHARINDEX(CAST(a.Id AS NVARCHAR(MAX)), @MaterList) > 0;

    -- 查询设施信息

    SELECT a.WorkShopName AS 车间名称,
           a.Environment AS 环境信息
    FROM dbo.EnterpriseFacilities AS a
    WHERE a.Id = @FacilityId;

    -- 查询成档案

    SELECT @GrowBatch = a.BatchNo
    FROM dbo.EnterpriseNote AS a
    WHERE Id = @GrowId;

    SELECT a.LvName AS 阶段名称,
           a.LvImg AS 阶段图片
    FROM dbo.EnterpriseAgeUp AS a
    WHERE a.BatchNo = @GrowBatch;

    SELECT a.SoilReport AS 土壤报告,
           a.AirReport AS 空气报告,
           a.WaterReport AS 水质报告,
           a.MetalReport AS 金属报告
    FROM dbo.EnterpriseEnvironmentAttach AS a
    WHERE a.BatchNo = @GrowBatch;

    IF (@CompanyType = 10)
    BEGIN
        SELECT a.FeedName AS 肥料名称,
               a.PlantTime AS 施肥时间,
               a.Producter AS 肥料生产商
        FROM dbo.EnterprisePlanting AS a
        WHERE a.IsType = 1
              AND a.BatchNo = @GrowBatch;

        SELECT a.DrugName AS 药品名称,
               a.PlantTime AS 施药时间,
               a.Producter AS 农药生产商
        FROM dbo.EnterpriseDrug AS a
        WHERE a.IsType = 1
              AND a.BatchNo = @GrowBatch;
    END;
    ELSE IF (@CompanyType = 20)
    BEGIN
        SELECT a.FeedName AS 肥料名称,
               a.PlantTime AS 施肥时间,
               a.Producter AS 肥料生产商
        FROM dbo.EnterprisePlanting AS a
        WHERE a.IsType = 2
              AND a.BatchNo = @GrowBatch;

        SELECT a.DrugName AS 疫苗名称,
               a.PlantTime AS 接种时间,
               a.Producter AS 疫苗生产商
        FROM dbo.EnterpriseDrug AS a
        WHERE a.IsType = 2
              AND a.BatchNo = @GrowBatch;
    END;

    -- 查询政府监管信息

    SELECT a.PotrolNum AS 抽查次数,
           a.BulletinNum AS 通报次数,
           a.QualifiedNum AS 合格率,
           (CASE a.BulletinNum
                WHEN 0 THEN
                    NULL
                ELSE
                    a.UpdateTime
            END
           ) AS 通报时间
    FROM dbo.GovtNetPatrol AS a
    WHERE a.CompanyId = @CompanyId;

    SELECT COUNT(a.Id) AS 投诉次数
    FROM dbo.GovtComplain AS a
    WHERE a.CompanyId = @CompanyId;

    SELECT a.RecoverStarTime AS 召回开始时间,
           a.RecoverEndTime AS 召回截至时间
    FROM dbo.EnterpriseRecover AS a
    WHERE a.CompanyId = @CompanyId
          AND a.RecoverGoodsName = @GoodsName;

    DROP TABLE #BaseInfo;
END;
GO
