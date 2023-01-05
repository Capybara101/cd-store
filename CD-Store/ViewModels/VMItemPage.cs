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
    class VMItemPage : VMBase
    {
        #region Properties
        private string productName;
        private string newProductCategory = "";
        private double productUnitPrice;
        string fileName;
        Category category = new Category();
        Product product = new Product();
        private ObservableCollection<Category> categoryList;
        private int categoryIDSelected;
        private BitmapImage imagen;


        public BitmapImage Imagen
        {
            get { return imagen; }
            set { imagen = value;OnPropertyChanged("Imagen"); }
        }
        public int CategoryIDSelected
        {
            get { return categoryIDSelected; }
            set { categoryIDSelected = value;OnPropertyChanged("CategoryIDSelected"); }
        }
        public ObservableCollection<Category> CategoryList
        {
            get { return categoryList; }
            set { categoryList = value; OnPropertyChanged("CategoryList"); }
        }
        public double ProductUnitPrice
        {
            get { return productUnitPrice; }
            set { productUnitPrice = value; }
        }

        public string NewProductCategory
        {
            get { return newProductCategory; }
            set { newProductCategory = value; OnPropertyChanged("NewProductCategory"); }
        }


        public string ProductName
        {
            get { return productName; }
            set { productName = value; OnPropertyChanged("ProductName"); }
        }
        #endregion
        #region contructor
        public VMItemPage()
        {
            GetCategories();
        }
        #endregion
        #region Commands
        public ICommand AddNewProductCategory
        {
            get
            {
                return new RelayCommand(() =>
                {
                    if (newProductCategory.Trim().Length == 0) { MessageBox.Show("Escriba un nombre para la categoría"); return; }
                    category.name = newProductCategory.ToUpper();
                    category.InsertCategory();
                    NewProductCategory = "";
                    MessageBox.Show("Categoría agregada con éxito");
                    GetCategories();
                });
            }
        }
        public ICommand AddNewProduct
        {
            get
            {
                return new RelayCommand(() =>
                {
                    try
                    {
                        product.name = ProductName.Trim().ToUpper();
                        product.categoryId = categoryIDSelected;
                        product.unitPrice = ProductUnitPrice;
                        int id = product.InsertProduct();
                        
                        /*if ( id> 0)
                        {
                            string ruta = Directory.GetCurrentDirectory()+ @"\imagenes";
                            if (!Directory.Exists(ruta))
                            {
                                Directory.CreateDirectory(ruta);
                            }
                            System.IO.File.Copy(fileName,ruta+"/"+id+".jpg" );
                            MessageBox.Show("Creado Correctamente");
                        }*/
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
