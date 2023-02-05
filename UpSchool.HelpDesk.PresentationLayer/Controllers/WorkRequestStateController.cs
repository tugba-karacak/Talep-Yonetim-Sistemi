using FastMember;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System.Data;
using System.Drawing.Printing;
using System.Reflection;
using System.Reflection.Metadata;
using UpSchool.HelpDesk.PresentationLayer.ApiServices;
using UpSchool.HelpDesk.PresentationLayer.Models;

namespace UpSchool.HelpDesk.PresentationLayer.Controllers
{
    public class WorkRequestStateController : Controller
    {
        private readonly WorkRequestStateApiService workRequestStateApiService;

        public WorkRequestStateController(WorkRequestStateApiService workRequestStateApiService)
        {
            this.workRequestStateApiService = workRequestStateApiService;

        }
        public async Task<IActionResult> Index(int workRequestId)
        {
            var result = await this.workRequestStateApiService.GetWorkRequestStatesAsync(workRequestId);

            ViewBag.WorkRequestId = workRequestId;
            return View(result.Data);
        }

        public async Task<IActionResult> ExcelFile(int workRequestId)
        {
            var result = await this.workRequestStateApiService.GetWorkRequestStatesAsync(workRequestId);

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            ExcelPackage excelPackage = new ExcelPackage();

            var excelModel = result.Data.Select(x => new ExcelPdfModel
            {
                Description = x.Description
            });

            var excelBank = excelPackage.Workbook.Worksheets.Add("Faaliyet Raporu");

            excelBank.Cells["A1"].LoadFromCollection(excelModel,true);

            var bytes = excelPackage.GetAsByteArray();
            return File(bytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", Guid.NewGuid() + ".xlsx");
        }

    

        public async Task<IActionResult> PdfFile(int workRequestId)
        {
            DataTable dataTable = new DataTable();

            var result = await this.workRequestStateApiService.GetWorkRequestStatesAsync(workRequestId);

            var pdfModel = result.Data.Select(x => new ExcelPdfModel
            {
                Description = x.Description
            });

            dataTable.Load(ObjectReader.Create(pdfModel));


            string fileName = Guid.NewGuid() + ".pdf";

            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/documents/" + fileName);
            var stream = new FileStream(path, FileMode.Create);



            var document = new iTextSharp.text.Document(PageSize.A4, 25f, 25f, 25f, 25f);

            PdfWriter.GetInstance(document, stream);

            document.Open();


            PdfPTable pdfPTable = new PdfPTable(dataTable.Columns.Count);

            for (int i = 0; i < dataTable.Columns.Count; i++)
            {
                pdfPTable.AddCell(dataTable.Columns[i].ColumnName);
            }


            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                for (int j = 0; j < dataTable.Columns.Count; j++)
                {
                    pdfPTable.AddCell(dataTable.Rows[i][j].ToString());
                }
            }



            document.Add(pdfPTable);

            document.Close();
            return File("/documents/" + fileName, "application/pdf", fileName);
        }

        public IActionResult Create(int workRequestId)
        {
            return View(new CreateWorkRequestStateModel { WorkRequestId = workRequestId });
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateWorkRequestStateModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await this.workRequestStateApiService.CreateAsync(model);

                return RedirectToAction("Index", new { @workRequestId = model.WorkRequestId });
            }
            return View(model);
        }

        public async Task<IActionResult> Update(int id, int workRequestId)
        {


            var result = await this.workRequestStateApiService.GetAsync(id);
            return View(new UpdateWorkRequestStateModel
            {
                Id = result.Data.Id,
                Description = result.Data.Description,
                WorkRequestId = workRequestId
            });
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateWorkRequestStateModel model)
        {

            if (ModelState.IsValid)
            {
                var result = await this.workRequestStateApiService.UpdateAsync(model);
                return RedirectToAction("Index", new { @workRequestId = model.WorkRequestId }); 
            }


            return View(model);
        }

        public async Task<IActionResult> Delete(int id, int workRequestId)
        {
            await this.workRequestStateApiService.DeleteAsync(id);
            return RedirectToAction("Index", new { @workRequestId = workRequestId });
        }

      
    }
}
