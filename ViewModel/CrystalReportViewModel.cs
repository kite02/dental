using CrystalDecisions.CrystalReports.Engine;
using SAPBusinessObjects.WPF.Viewer;
using SmileLineDentalClinic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmileLineDentalClinic.ViewModel
{
    public class CrystalReportViewModel:ValidatableModel
    {
        private string title;

        public string Title
        {
            get { return title; }
            set { title = value;
                RaisePropertyChanged("Title");
            }
        }

        private ReportDocument report;

        public ReportDocument Report
        {
            get { return report; }
            set { report = value;
                RaisePropertyChanged("ReportSource");
            }
        }







        public CrystalReportViewModel()
        {
            
        }


    }
}
