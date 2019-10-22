using System;
using System.Collections.Generic;
using System.Text;

namespace KilyCore.DataEntity.ResponseMapper.Phone
{
    public class SQLHelper
    {
        public static string SQLBase = @"SELECT 绑定记录表.Id AS 二维码,
       绑定记录表.StarSerialNo AS 开始,
       绑定记录表.EndSerialNo AS 结束,
       产品表.LineCode AS 产品编号,
       产品表.ProductName AS 产品名称,
       产品表.ProductType AS 产品类型,
       产品表.Image AS 产品图片集合,
       产品表.ExpiredDate AS 保质期,
       产品表.Price AS 产品价格,
       产品表.SellWebNet AS 销售网址,
       产品表.Spec AS 规格,
       产品表.Unit AS 计量单位,
       产品表.ProductSeriesId AS 产品系列,
       入库表.GoodsBatchNo AS 入库批次,
       入库表.ProductTime AS 生产时间,
       入库表.GrowNoteId AS 成长档案,
       入库表.ImgUrl AS 产品图片,
       入库表.Remark AS 产品介绍,
       入库表.BuyId AS 进货信息,
       出库表.GoodsBatchNo AS 出库批次,
       出库表.OutStockTime AS 出库时间,
       企业表.Id AS 企业,
       企业表.CompanyName AS 企业名称,
       (CASE 企业表.CompanyType
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
       企业表.CompanyType AS 企业类型,
       企业表.CompanyAddress AS 企业地址,
       企业表.CompanyPhone AS 企业电话,
       企业表.ComplainPhone AS 投诉电话,
       企业表.MainPro AS 主要产品,
	   企业表.Scope AS 主要经营,
       企业表.MainProRemark AS 主要介绍,
       企业表.ComImage AS 企业形象,
       企业表.SafeOffer AS 安全员,
       企业表.OfferLv AS 安全等级,
       企业表.CompanySafeLv AS 企业等级,
       企业表.CardExpiredDate AS 证件到期,
       企业表.LngAndLat AS 经纬度,
       企业表.Discription AS 企业介绍,
       企业表.Certification AS 营业执照,
       企业表.VideoAddress AS 企业视频,
       企业表.HonorCertification AS 荣誉证书,
       企业表.CommunityCode AS 工商代码,
       企业表.NetAddress AS 企业网站,
       企业表.TypePath AS 所在区域,
       仓库表.StockName AS 仓库名称,
       仓库表.SaveType AS 贮藏方式,
       仓库表.SaveTemp AS 贮藏温度,
       仓库表.SaveH2 AS 贮藏湿度,
       质检表.CheckName AS 报告名称,
       质检表.CheckUint AS 质检单位,
       质检表.CheckUser AS 质检人,
       质检表.CheckResult AS 质检结果,
       质检表.CheckReport AS 质检报告
FROM dbo.EnterpriseTagAttach AS 绑定记录表
    JOIN dbo.EnterpriseGoods AS 产品表
        ON 绑定记录表.GoodsId = 产品表.Id
    JOIN dbo.EnterpriseGoodsStock AS 入库表
        ON 绑定记录表.StockNo = 入库表.GoodsBatchNo
    JOIN dbo.EnterpriseGoodsStockAttach AS 出库表
        ON 入库表.Id = 出库表.StockId
    JOIN dbo.EnterpriseStockType AS 仓库表
        ON 入库表.StockTypeId = 仓库表.Id
    JOIN dbo.EnterpriseInfo AS 企业表
        ON 企业表.Id = 绑定记录表.CompanyId
    JOIN dbo.EnterpriseCheckGoods AS 质检表
        ON 质检表.Id = 入库表.CheckGoodsId
WHERE 绑定记录表.StarSerialNo <= @Code
      AND 绑定记录表.EndSerialNo >= @Code
      AND 产品表.AuditType = 40
      AND 绑定记录表.TagType = @CodeType";
        public static string CodeStar = @" AND 企业表.CodeStar = @Fix;";
        public static string SQLMaterial = @"SELECT a.Id AS 原料,
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
    WHERE CHARINDEX(CAST(a.Id AS NVARCHAR(MAX)), @Ids) > 0;";
    }
}
