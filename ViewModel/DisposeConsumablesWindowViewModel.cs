using GalaSoft.MvvmLight.CommandWpf;
using SmileLineDentalClinic.Model;
using SmileLineDentalClinic.View;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmileLineDentalClinic.ViewModel
{
    public class DisposeConsumablesWindowViewModel:ValidatableModel
    {

        private int existedquantity;

        public int ExistedQuantity
        {
            get { return existedquantity; }
            set { existedquantity = value;
                RaisePropertyChanged("ExistedQuantity");
            }
        }


        private int quantity;
        [CustomValidation(typeof(DisposeConsumablesWindowViewModel), "DisposeValidate")]
        public int Quantity
        {
            get { return quantity; }
            set { quantity = value;
                RaisePropertyChanged("Quantity");
            }
        }

        private DisposeConsumablesWindowView mywindow;

        public DisposeConsumablesWindowView MyWindow
        {
            get { return mywindow; }
            set
            {
                mywindow = value;
                RaisePropertyChanged("MyWindow");
            }
        }

        private bool isaccepted;

        public bool IsAccepted
        {
            get { return isaccepted; }
            set { isaccepted = value;
                RaisePropertyChanged("IsAccepted");
            }
        }


        public RelayCommand Accept { get; set; }

        public DisposeConsumablesWindowViewModel()
        {
            Accept = new RelayCommand(accept,canaccept);
            IsAccepted = false;
            MyWindow = null;
        }

        private void accept()
        {
            IsAccepted = true;
            MyWindow.Close();
        }

        private bool canaccept()
        {
            return this.HasErrors == false;
        }

        public static ValidationResult DisposeValidate(object obj, ValidationContext context)
        {

            var disposeconsumableswindowviewmodel = (DisposeConsumablesWindowViewModel)context.ObjectInstance;
            if (disposeconsumableswindowviewmodel.ExistedQuantity < disposeconsumableswindowviewmodel.Quantity)
            {
                    return new ValidationResult("Dispose Quantity is greater than Existed Quantity",
                    new List<string> { "Quantity" });
            }
            
            return ValidationResult.Success;

        }


    }
}
