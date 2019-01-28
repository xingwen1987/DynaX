using System;

namespace AspNetCore.DynaX.DbContexts.Web.Models
{
    public class WarehouseInfo
    {
        /// <summary>
        /// 仓库 Id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 仓库名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 省份
        /// </summary>
        public string Province { get; set; }

        /// <summary>
        /// 城市
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// 仓库地址
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 仓库经度
        /// </summary>
        public decimal Longitude { get; set; }

        /// <summary>
        /// 仓库纬度
        /// </summary>
        public decimal Latitude { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateDateTime { get; set; }

        /// <summary>
        /// 仓库删除
        /// </summary>
        public bool? Recycle { get; set; }

    }
}
