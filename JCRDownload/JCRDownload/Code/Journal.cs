using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JCRDownload.Code
{
    public class Journal
    {
        /// <summary>
        /// 详细页URL
        /// </summary>
        public string DetailURL { set; get; }
        /// <summary>
        /// 所在列表页URL
        /// </summary>
        public string ListURL { set; get; }
        /// <summary>
        /// 期刊全称
        /// </summary>
        public string Title { set; get; }
        /// <summary>
        /// JCR简称
        /// </summary>
        public string JCRAbbreviatedTitle { set; get; }
        /// <summary>
        /// ISO简称
        /// </summary>
        public string ISOAbbreviatedTitle { set; get; }
        public string ISSN { set; get; }
        public string Issues { set; get; }
        public string Language { set; get; }
        public string Country { set; get; }
        public string Publisher { set; get; }
        public string Address { set; get; }
        /// <summary>
        /// 学科，采集时的学科
        /// </summary>
        public string Category { set; get; }
        /// <summary>
        /// 学科分区
        /// </summary>
        public string CategoryRank { set; get; }
        public int TotalCites { set; get; }
        public double ImpactFactor { set; get; }
        public double ImpactFactorFor5years { set; get; }
        public double ImmediacyIndex { set; get; }
        public int Aritcles { set; get; }
        /// <summary>
        /// 引用半周期，数据中会出现>10.0,一律处理为10.1
        /// </summary>
        public double CitedHalfLife { set; get; }
        public double EigenfactorScore { set; get; }
        public double ArticleInfluenceScore { set; get; }
    }
}
