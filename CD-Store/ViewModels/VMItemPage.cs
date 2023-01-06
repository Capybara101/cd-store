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
        private string categoryVisivility = "Hidden";





        public BitmapImage Imagen
        {
            get { return imagen; }
            set { imagen = value; OnPropertyChanged("Imagen"); }
        }
        public string CategoryVisivility
        {
            get { return categoryVisivility; }
            set { categoryVisivility = value; OnPropertyChanged("CategoryVisivility"); }
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
                    CategoryVisivility = "Hidden";
                    GetCategories();
                });
            }
        }
        public ICommand CategoryVisivilityCommand
        {
            get
            {
                return new RelayCommand(()=>
                {
                    if (CategoryVisivility == "Visible") CategoryVisivility = "Hidden";
                    else CategoryVisivility = "Visible";


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
                        if(ProductName!=string.Empty && categoryIDSelected!=0 && productUnitPrice > 0 && fileName!=null && fileName!=string.Empty )
                        {
                            product.name = ProductName.Trim().ToUpper();
                            product.categoryId = categoryIDSelected;
                            product.unitPrice = ProductUnitPrice;
                            int id = product.InsertProduct();
                            if (id > 0)
                            {
                                string path = Directory.GetCurrentDirectory() + @"\imagenes";
                                if (!Directory.Exists(path))
                                {
                                    Directory.CreateDirectory(path);
                                }
                                System.IO.File.Copy(fileName, path + @"\" + id + ".jpg");
                                MessageBox.Show("Creado Correctamente");
                                ProductName = "";
                                ProductUnitPrice = 0;
                                Imagen = null;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Debe llenar todos los Campos de Texto y seleccionar una Imagen");
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
