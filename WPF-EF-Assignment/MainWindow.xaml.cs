using Microsoft.Extensions.DependencyInjection;
using Microsoft.Win32;
using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPF_EF_Assignment.Data;
using WPF_EF_Assignment.Repositories;
using System.Data.Entity;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Parsing;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Grid;
using System.Data;
using System.Drawing;

namespace WPF_EF_Assignment
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ILogger _logger;
        List<ReagentLot> BatchList = new List<ReagentLot>();
        IUnitOfWork _unitOfWork;
        public MainWindow()
        {
            _unitOfWork = new UnitOfWork(new AppDbContext());
            _logger = LogManager.GetCurrentClassLogger();
            InitializeComponent();
            LoadLotDetails();
        }

        private void LoadLotDetails()
        {
            try
            {
                var lotBatch = _unitOfWork.ReagentLotRepository.GetWithInclude("ReagentBatch").ToList();
                DataGrid.ItemsSource = lotBatch.Select(lb => new DataItems
                {
                    BatchLotNumber = lb.ReagentBatch.BatchLotNumber,
                    BatchExpiryDate = lb.ReagentBatch.ExpiryDate.ToString("yyyy-MM-dd"),
                    Manufacturer = lb.ReagentBatch.Manufacturer,
                    ManufacturerDate = lb.ReagentBatch.ManufacturerDate.ToString("yyyy-MM-dd"),
                    ManufacturingSourceCode = lb.ReagentBatch.ManufacturingSourceCode,
                    SerialNumber = lb.SerialNumber,
                    LotName = lb.Name,
                    ExpiryDate = lb.ExpiryDate.ToString("yyyy-MM-dd"),
                    Volume = lb.Volume,
                    ReactionTarget = lb.ReactionTarget,
                    ReactionRange = lb.ReactionRange,
                }).ToList();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
        }

        private void BrowseFile(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
                BrowseTxt.Text = openFileDialog.FileName;
        }

        private void UploadData(object sender, RoutedEventArgs e)
        {
            try
            {
                if (BrowseTxt.Text != null && BrowseTxt.Text != string.Empty)
                {
                    var records = System.IO.File
                                    .ReadAllLines(BrowseTxt.Text)
                                    .Select(record => record.Split('|'))
                                    .Select(record => new
                                    {
                                        CountyCode = record[0],
                                        SourceFactoryCode = record[1],
                                        SerialNumber = record[2],
                                        BatchLotNumber = record[3],
                                        BatchExpiryDate = record[4],
                                        Manufacturer = record[5],
                                        ManufacturerDate = record[6],
                                        ManufacturingSourceCode = record[7],
                                        LotName = record[8],
                                        ExpiryDate = record[9],
                                        Volume = record[10],
                                        ReactionTarget = record[11],
                                        ReactionRange = record[12],
                                    }).ToList();

                    records = records.Where(c => c.CountyCode != "CountyCode").ToList();
                    var reagentBatch = records.GroupBy(c => c.BatchLotNumber, (key, c) => c.FirstOrDefault());
                    foreach (var batch in reagentBatch)
                    {
                        var lotBatch = new ReagentBatch
                        {
                            BatchLotNumber = batch.BatchLotNumber,
                            ExpiryDate = Convert.ToDateTime(batch.BatchExpiryDate),
                            Manufacturer = batch.Manufacturer,
                            ManufacturerDate = Convert.ToDateTime(batch.ManufacturerDate),
                            ManufacturingSourceCode = batch.ManufacturingSourceCode,
                            ReagentLot = records.Where(b => b.BatchLotNumber == batch.BatchLotNumber)
                                                .Select(r => new ReagentLot
                                                {
                                                    SerialNumber = r.SerialNumber,
                                                    Name = r.LotName,
                                                    ExpiryDate = Convert.ToDateTime(r.ExpiryDate),
                                                    ReactionRange = Convert.ToDouble(r.ReactionRange),
                                                    ReactionTarget = Convert.ToDouble(r.ReactionTarget),
                                                    Volume = Convert.ToDouble(r.Volume)
                                                }).ToList()
                        };
                        _unitOfWork.ReagentBatchRepository.Insert(lotBatch);
                    }
                    _unitOfWork.Save();
                }
                MessageBox.Show("Succefully Uploaded data into the system!");
                LoadLotDetails();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                MessageBox.Show("Uploaded data wasn't Succefully; please verify app logs to find out more!");
                //throw ex;
            }
        }

        private void PrintToPdf(object sender, RoutedEventArgs e)
        {
            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();

                saveFileDialog.Filter = "PDF Files (*.pdf)|*.pdf";
                saveFileDialog.DefaultExt = "pdf";
                saveFileDialog.AddExtension = true;

                if (saveFileDialog.ShowDialog() == true)
                {
                    //Create a new PDF document
                    PdfDocument doc = new PdfDocument();
                    //Add a page
                    PdfPage page = doc.Pages.Add();
                    //Create a PdfGrid
                    PdfGrid pdfGrid = new PdfGrid();
                    //Assign data source
                    pdfGrid.DataSource = DataGrid.ItemsSource;
                    //Draw grid to the page of PDF document
                    pdfGrid.Draw(page, new PointF(10, 10));
                    //Save the document
                    doc.Save(saveFileDialog.FileName);
                    //Close the document
                    doc.Close(true);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
        }
    }
    public class DataItems
    {
        public string BatchLotNumber { get; set; }
        public string BatchExpiryDate { get; set; }
        public string Manufacturer { get; set; }
        public string ManufacturerDate { get; set; }
        public string ManufacturingSourceCode { get; set; }
        public string SerialNumber { get; set; }
        public string LotName { get; set; }
        public string ExpiryDate { get; set; }
        public double Volume { get; set; }
        public double ReactionTarget { get; set; }
        public double ReactionRange { get; set; }
    }
}
