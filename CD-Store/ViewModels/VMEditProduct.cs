using CD_Store.Models;
using GalaSoft.MvvmLight.Command;
using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace CD_Store.ViewModels
{
    class VMEditProduct : VMBase
    {
        
        #region Properties
        private string productName;
        private double productUnitPrice;
        string fileName;
        Category category = new Category();
        Product productU = new Product();
        private ObservableCollection<Category> categoryList;
        private int categoryIDSelected;
        private BitmapImage imagen;



        public BitmapImage Imagen
        {
            get { return imagen; }
            set { imagen = value; OnPropertyChanged("Imagen"); }
        }
        public int CategoryIDSelected
        {
            get { return categoryIDSelected; }
            set { categoryIDSelected = value; OnPropertyChanged("CategoryIDSelected"); }
        }
        public ObservableCollection<Category> CategoryList
        {
            get { return categoryList; }
            set { categoryList = value; OnPropertyChanged("CategoryList"); }
        }
        public double ProductUnitPrice
        {
            get { return productUnitPrice; }
            set { productUnitPrice = value; OnPropertyChanged("ProductUnitPrice"); }
        }

        public string ProductName
        {
            get { return productName; }
            set { productName = value; OnPropertyChanged("ProductName"); }
        }
        #endregion
        #region contructor
        public VMEditProduct(Product product)
        { 
            GetCategories();
            ProductName = product.name;
            ProductUnitPrice = product.unitPrice;
            Imagen=new BitmapImage(new Uri(product.productPath));
            productU.productId = product.productId;
        }
        #endregion
        #region Commands
     
        public ICommand EditProduct
        {
            get
            {
                return new RelayCommand(() =>
                {
                    try
                    {

                        if (ProductName != string.Empty && categoryIDSelected != 0 && productUnitPrice > 0 && fileName != null && fileName != string.Empty)
                        {
                            productU.name = ProductName.Trim().ToUpper();
                            productU.categoryId = categoryIDSelected;
                            productU.unitPrice = ProductUnitPrice;
                            int res = productU.UpdateProduct();
                            if (res > 0)
                            {

                                string path = Directory.GetCurrentDirectory() + @"\imagenes";
                                if (!Directory.Exists(path))
                                {
                                    Directory.CreateDirectory(path);
                                }
                                System.IO.File.Copy(fileName, path + @"\" + productU.productId + ".jpg",true);
                                MessageBox.Show("Creado Correctamente");
                                ProductName = "";
                                ProductUnitPrice = 0;
                                Imagen = null;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Debe Llenar Todos Los Campos de Texto Y Selecionar Una Imagen");
                        }

                    }
                    catch (System.Exception e)
                    {
                        MessageBox.Show(e.Message);
                    }
                });
            }
        }
        public ICommand AddProductImage
        {
            get
            {
                return new RelayCommand(() =>
                {
                    try
                    {
                        OpenFileDialog ofd = new OpenFileDialog();
                        ofd.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
                        if (ofd.ShowDialog() == true)
                        {
                            fileName = ofd.FileName;
                            Imagen = new BitmapImage(new Uri(fileName));
                        }
                    }
                    catch (System.Exception e)
                    {
                        MessageBox.Show(e.Message);
                    }

                });
            }
        }
        private void GetCategories()
        {
            CategoryList = category.ReadCategoryTable();
        }
        #endregion
    
    }
}
