using CD_Store.Models;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace CD_Store.ViewModels
{
    class VMReports : VMBase
    {
        private ObservableCollection<ReportDetail> datalist = new ObservableCollection<ReportDetail>();
        private string fechaInicio;
        private string fechaFinal;
        private ObservableCollection<Category> categoryList;
        private int categoryIDSelected;
        private double totalVentas;

        public double TotalVentas
        {
            get { return totalVentas; }
            set { totalVentas = value; OnPropertyChanged("TotalVentas"); }
        }

        public ObservableCollection<ReportDetail> Datalist
        {
			get { return datalist; }
			set { datalist = value; OnPropertyChanged("Datalist"); }
        }

        public string FechaInicio
        {
            get { return fechaInicio; }
            set { fechaInicio = value; }
        }

        public string FechaFinal
        {
            get { return fechaFinal; }
            set { fechaFinal = value; }
        }

        public ObservableCollection<Category> CategoryList
        {
            get { return categoryList; }
            set { categoryList = value; OnPropertyChanged("CategoryList"); }
        }

        public int CategoryIDSelected
        {
            get { return categoryIDSelected; }
            set { categoryIDSelected = value; OnPropertyChanged("CategoryIDSelected"); }
        }

        public VMReports()
        {
            CategoryList = new ObservableCollection<Category>();
            TotalVentas = 0;
            CategoryIDSelected = 0;
            GetCategories();
            foreach (SaleDetail saleDetail in new SaleDetail().ReadSaleDetailTable("", "", ""))
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
                TotalVentas += saleDetail.quantity * saleDetail.unitPrice;
			}
        }

        public ICommand FilterSales
        {
            get
            {
                return new RelayCommand(() =>
                {
                    TotalVentas = 0;
                    if ((!string.IsNullOrWhiteSpace(FechaInicio) && !DateTime.TryParseExact(FechaInicio, "dd/MM/yyyy", new CultureInfo("en-US"), DateTimeStyles.None, out DateTime dateValue1))
                        || (!string.IsNullOrWhiteSpace(FechaFinal) && !DateTime.TryParseExact(FechaFinal, "dd/MM/yyyy", new CultureInfo("en-US"), DateTimeStyles.None, out DateTime dateValue2)))
                    {
                        MessageBox.Show("La fecha debe tener el formato dd/MM/yyyy.\n(Por ejemplo: 24/09/2000)", "La fecha está en el formato incorrecto.", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    }
                    else
                    {
                        Datalist.Clear();
                        string CategoryIDString = "";
                        if (CategoryIDSelected > 0) CategoryIDString = CategoryIDSelected.ToString();
                        foreach (SaleDetail saleDetail in new SaleDetail().ReadSaleDetailTable(FechaInicio, FechaFinal, CategoryIDString))
                        {
                            Datalist.Add(new ReportDetail {
                                Nombre = new Product().GetName(saleDetail.productId),
                                Cantidad = saleDetail.quantity,
                                Precio_Unitario = saleDetail.unitPrice,
                                Total = saleDetail.quantity * saleDetail.unitPrice,
                                Fecha_Registro = saleDetail.registerDate
                            });
                            TotalVentas += saleDetail.quantity * saleDetail.unitPrice;
                        }
                    }
                });
            }
        }

        Category category = new Category();
        private void GetCategories()
        {
            CategoryList.Add(new Category(0, "Todas las Categorias"));
            foreach (Category cat in category.ReadCategoryTable()) CategoryList.Add(cat); 
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