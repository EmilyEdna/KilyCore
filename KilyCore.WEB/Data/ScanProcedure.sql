USE [Kily]
GO

/****** Object:  StoredProcedure [dbo].[Sp_GetScanCodeInfo]    Script Date: 2019/2/15 11:21:29 ******/
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
    @CodeType INT NULL,
    @PreFix NVARCHAR(MAX) NULL
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
    DECLARE @TackPackNo NVARCHAR(MAX);
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
           e.CodeStar AS 号段前缀,
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
		   e.ComplainPhone AS 投诉电话,
           g.StartTime AS 生产时间,
           g.Id AS 生产批次Id,
           g.BatchNo AS 生产批次,
           g.Manager AS 生产负责人,
           g.MaterialId AS 原料列表,
           g.DeviceName AS 设备名称,
           n.SupplierName AS 设备生产商,
           n.Manager AS 设备负责人,
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
           m.IdentStar AS 认证星级,
           k.Id AS 装车Id,
           k.PackageNo AS 装车批次
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
        LEFT JOIN dbo.EnterpriseProductSeries AS f
            ON f.Id = b.ProductSeriesId
        LEFT JOIN dbo.EnterpriseProductionBatch AS g
            ON g.SeriesId = f.Id
        LEFT JOIN dbo.EnterpriseStockType AS h
            ON h.Id = c.StockTypeId
        LEFT JOIN dbo.EnterpriseBuyer AS i
            ON e.Id = i.CompanyId
        LEFT JOIN dbo.EnterpriseCheckGoods j
            ON j.Id = c.CheckGoodsId
        LEFT JOIN dbo.EnterpriseGoodsPackage AS k
            ON d.GoodsBatchNo = k.ProductOutStockNo
        LEFT JOIN dbo.EnterpriseLogistics AS l
            ON k.PackageNo = l.PackageNo
        LEFT JOIN dbo.EnterpriseIdent AS m
            ON e.Id = m.CompanyId
        LEFT JOIN dbo.EnterpriseDevice AS n
            ON n.DeviceName = g.DeviceName
    WHERE a.IsDelete = 0
          AND a.TagType = @CodeType
          AND a.StarSerialNo <= @Code
          AND a.EndSerialNo >= @Code
          AND a.StockNo = c.GoodsBatchNo
          AND b.AuditType = 40;

    IF (@Id IS NOT NULL)
    BEGIN
        IF (@PreFix IS NOT NULL)
        BEGIN
            SELECT a.*
            FROM #BaseInfo AS a
            WHERE a.二维码Id = @Id
                  AND a.号段前缀 = @PreFix;
            SELECT @CompanyId = a.企业Id,
                   @CompanyType = a.企业类型,
                   @GoodsName = a.产品名称,
                   @ProductionBatchId = a.生产批次Id,
                   @MaterList = a.原料列表,
                   @FacilityId = a.设施Id,
                   @GrowId = a.成长Id,
                   @TackPackNo = a.装车批次
            FROM #BaseInfo AS a
            WHERE a.号段前缀 = @PreFix;
        END;
        ELSE
        BEGIN
            SELECT a.*
            FROM #BaseInfo AS a
            WHERE a.二维码Id = @Id;
            SELECT @CompanyId = a.企业Id,
                   @CompanyType = a.企业类型,
                   @GoodsName = a.产品名称,
                   @ProductionBatchId = a.生产批次Id,
                   @MaterList = a.原料列表,
                   @FacilityId = a.设施Id,
                   @GrowId = a.成长Id,
                   @TackPackNo = a.装车批次
            FROM #BaseInfo AS a
            WHERE a.二维码Id = @Id;
        END;
    END;
    ELSE
    BEGIN
        IF (@PreFix IS NOT NULL)
        BEGIN
            SELECT a.*
            FROM #BaseInfo AS a
            WHERE a.号段前缀 = @PreFix;
            SELECT @CompanyId = a.企业Id,
                   @CompanyType = a.企业类型,
                   @GoodsName = a.产品名称,
                   @ProductionBatchId = a.生产批次Id,
                   @MaterList = a.原料列表,
                   @FacilityId = a.设施Id,
                   @GrowId = a.成长Id,
                   @TackPackNo = a.装车批次
            FROM #BaseInfo AS a
            WHERE a.号段前缀 = @PreFix;
        END;
        ELSE
        BEGIN
            SELECT a.*
            FROM #BaseInfo AS a;
            SELECT @CompanyId = a.企业Id,
                   @CompanyType = a.企业类型,
                   @GoodsName = a.产品名称,
                   @ProductionBatchId = a.生产批次Id,
                   @MaterList = a.原料列表,
                   @FacilityId = a.设施Id,
                   @GrowId = a.成长Id,
                   @TackPackNo = a.装车批次
            FROM #BaseInfo AS a;
        END;
    END;
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
           c.SetStockTime AS 采购时间,
           c.ProductTime AS 原料生产时间,
           b.CheckUint AS 质检单位,
           b.CheckUser AS 质检人,
           b.CheckReport AS 质检报告,
           b.CheckResult AS 质检结果
    FROM dbo.EnterpriseMaterial AS a
        LEFT JOIN dbo.EnterpriseCheckMaterial AS b
            ON a.Id = b.MaterId
        JOIN dbo.EnterpriseMaterialStock AS c
            ON a.BatchNo = c.BatchNo
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

    SELECT a.CheckReport AS 成长档案质检报告,
           a.BatchNo AS 批次编号,
		   a.ResultTime AS 收获日期
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

    SELECT TOP 3
       a.AirEnv AS 空气温度,
       a.AirHdy AS 空气湿度,
       a.SoilEnv AS 土壤温度,
       a.SoilHdy AS 土壤湿度,
       CAST((CONVERT(DECIMAL(18, 2), (CONVERT(DECIMAL, a.Light) / 100))) AS NVARCHAR(MAX)) AS 光照,
       CAST((CONVERT(DECIMAL(18, 2), (CONVERT(DECIMAL, a.CO2) / 100))) AS NVARCHAR(MAX)) AS 二氧化碳
    FROM dbo.EnterpriseEnvironment AS a
    WHERE a.BatchNo = @GrowBatch
	ORDER BY a.RecordTime DESC;

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

    -- 查询扫码次数

    SELECT SUM(a.ScanNum) AS 扫码次数
    FROM dbo.EnterpriseScanCodeInfo AS a
    WHERE a.ScanPackageNo = @TackPackNo;

    DROP TABLE #BaseInfo;
END;
GO






USE [KilyTest]
GO
/****** Object:  StoredProcedure [dbo].[Sp_GetScanMerchantInfo]    Script Date: 2019/8/7 11:57:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		刘泽华
-- Create date: 2019年1月7日14点36分
-- Description:	餐饮企业手机扫码
-- =============================================
CREATE PROCEDURE [dbo].[Sp_GetScanMerchantInfo]
(@Id UNIQUEIDENTIFIER NULL)
AS
BEGIN
    DECLARE @MerchantId UNIQUEIDENTIFIER;
    DECLARE @FoodDishIds NVARCHAR(MAX);
    DECLARE @StuffIds NVARCHAR(MAX);
    DECLARE @VedioIds NVARCHAR(MAX);
    DECLARE @UserIds NVARCHAR(MAX);
    DECLARE @DuckIds NVARCHAR(MAX);
    DECLARE @DrawIds NVARCHAR(MAX);
    DECLARE @SampleIds NVARCHAR(MAX);
    DECLARE @DisinfectIds NVARCHAR(MAX);
    DECLARE @AdditiveIds NVARCHAR(MAX);
    DECLARE @TicketsIds NVARCHAR(MAX);
    DECLARE @WeekMenusIds NVARCHAR(MAX);

        SELECT TOP 1
           @MerchantId = a.InfoId,
           @FoodDishIds = a.DishIds,
           @StuffIds = a.StuffIds,
           @VedioIds = a.VideoIds,
           @UserIds = a.UserIds,
           @DuckIds = a.DuckIds,
           @DrawIds = a.DrawIds,
           @SampleIds = a.SampleIds,
           @DisinfectIds = a.DisinfectIds,
           @AdditiveIds = a.AdditiveIds,
           @TicketsIds = a.Tickets,
           @WeekMenusIds = a.WeekMenus
    FROM dbo.RepastScanInfo AS a
    WHERE (
              a.InfoId = @Id
              OR a.Id = @Id
          )
          AND a.IsDelete = 1
    ORDER BY a.CreateTime DESC;

    -- 商家信息

    SELECT a.Id AS 商家Id,
           a.MerchantName AS 商家名称,
           a.Certification AS 营业执照,
           a.Address AS 商家地址,
           a.Phone AS 商家电话,
           a.MerchantImage AS 商家形象,
           a.Remark AS 商家介绍
    FROM dbo.RepastInfo AS a
    WHERE a.Id = @MerchantId;

    -- 菜品信息

    SELECT a.DishName AS 菜品名称,
           a.DishType AS 菜品类型,
           a.Batching AS 配料,
           a.MainBatch AS 主料,
           a.Seasoning AS 调料,
           a.Remark AS 菜品介绍
    FROM dbo.RepastDish AS a
    WHERE CHARINDEX(CAST(a.Id AS NVARCHAR(MAX)), @FoodDishIds) > 0;

    -- 溯源信息

    SELECT a.MaterialName AS 原料名称,
           a.Supplier AS 供应商,
           a.SuppTime AS 供应时间,
           a.QualityReport AS 质检报告,
           a.ExpiredDay AS 保质期,
           a.Address AS 生产地址,
           a.BuyUser AS 采购人
    FROM dbo.RepastStuff AS a
    WHERE CHARINDEX(CAST(a.Id AS NVARCHAR(MAX)), @StuffIds) > 0;

    -- 视频信息

    SELECT a.MonitorAddress AS 监控区域,
           a.VideoAddress AS 视频连接
    FROM dbo.RepastVideo AS a
    WHERE CHARINDEX(CAST(a.Id AS NVARCHAR(MAX)), @VedioIds) > 0;

    -- 人员信息

    SELECT a.TrueName AS 人员姓名,
           a.Phone AS 人员电话,
		   a.ExpiredTime AS 健康证到期时间,
           a.HealthCard AS 健康证
    FROM dbo.RepastInfoUser AS a
    WHERE CHARINDEX(CAST(a.Id AS NVARCHAR(MAX)), @UserIds) > 0;

    -- 废物处理

    SELECT a.HandleWays AS 处理方式,
           a.HandleTime AS 处理时间,
           a.HandleUser AS 处理人,
           a.HandleImg AS 相关图片
    FROM dbo.RepastDuck AS a
    WHERE CHARINDEX(CAST(a.Id AS NVARCHAR(MAX)), @DuckIds) > 0;

    -- 抽样信息

    SELECT a.DrawUnit AS 抽样单位,
           a.DrawUser AS 抽样负责人,
           a.DrawTime AS 抽样时间,
           a.Phone AS 负责人电话
    FROM dbo.RepastDraw AS a
    WHERE CHARINDEX(CAST(a.Id AS NVARCHAR(MAX)), @DrawIds) > 0;

    -- 留样信息

    SELECT a.DishName AS 留样菜品,
           a.SampleTime AS 留样时间,
           a.OperatUser AS 操作人,
           a.Phone AS 操作人电话,
           a.SampleImg AS 留样图片
    FROM dbo.RepastSample AS a
    WHERE CHARINDEX(CAST(a.Id AS NVARCHAR(MAX)), @SampleIds) > 0;

    -- 消毒信息

    SELECT a.DisinfectName AS 消毒剂名称,
           a.Metering AS 使用计量,
           a.DisinfectTime AS 消毒时间,
           a.UsePerson AS 使用人,
           a.SupplierName AS 生产商
    FROM dbo.RepastDisinfect AS a
    WHERE CHARINDEX(CAST(a.Id AS NVARCHAR(MAX)), @DisinfectIds) > 0;

    -- 添加剂信息

    SELECT a.AdditiveName AS 添加剂名称,
           a.Metering AS 使用计量,
           a.ProFood AS 产物,
           a.UsePerson AS 使用人,
           a.SupplierName AS 生产商
    FROM dbo.RepastAdditive AS a
    WHERE CHARINDEX(CAST(a.Id AS NVARCHAR(MAX)), @AdditiveIds) > 0;

    -- 台账凭证

    SELECT a.Theme AS 台账主题,
           a.UpTime AS 上报时间,
           a.Content AS 台账内容
    FROM dbo.RepastBillTicket AS a
    WHERE CHARINDEX(CAST(a.Id AS NVARCHAR(MAX)), @TicketsIds) > 0;

    -- 周菜谱

    SELECT a.FoodMenuName AS 周菜谱名称,
           a.UpTime AS 上报时间,
           a.Content AS 周菜谱内容
    FROM dbo.RepastFoodMenu AS a
    WHERE CHARINDEX(CAST(a.Id AS NVARCHAR(MAX)), @WeekMenusIds) > 0;
END;
