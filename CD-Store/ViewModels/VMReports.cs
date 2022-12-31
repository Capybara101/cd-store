using CD_Store.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace CD_Store.ViewModels
{
    class VMReports : VMBase
    {
		private ObservableCollection<ReportDetail> datalist = new ObservableCollection<ReportDetail>();

		public ObservableCollection<ReportDetail> Datalist
        {
			get { return datalist; }
			set { datalist = value; OnPropertyChanged("Datalist"); }
		}

		public VMReports()
		{
            foreach(SaleDetail saleDetail in new SaleDetail().ReadSaleDetailTable())
			{
				Datalist.Add(
					new ReportDetail
					{
						Nombre = new Product().GetName(saleDetail.productId),
						Cantidad = saleDetail.quantity,
                        Precio_Unitario = saleDetail.unitPrice,
						Total = saleDetail.quantity * saleDetail.unitPrice,
                        Fecha_Registro = saleDetail.registerDate
					}
				);
			}
        }

        public class ReportDetail
        {
            public string Nombre { get; set; }
			public int Cantidad { get; set; }
			public double Precio_Unitario { get; set; }
			public double Total { get; set; }
            public DateTime Fecha_Registro { get; set; }
        }
	}
}