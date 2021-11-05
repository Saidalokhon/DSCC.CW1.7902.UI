using DSCC.CW1._7902.UI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace DSCC.CW1._7902.UI.Controllers
{
    // Create abstract class for base controller that extends Controller.
    // The type constraints are set to TModel
    public abstract class BaseController<TModel> : Controller
        where TModel : class, IModel
    {
        #region Private variables and constructor
        // Create base Uri string for HttpClient
        private string BaseUri = "https://localhost:44381/api/";
        // Create private string _modelName to determine which model should
        // controller use.
        private string _modelName;

        // Create constructor to initialize _modelName.
        public BaseController(string modelName)
        {
            _modelName = modelName;
        }
        #endregion
        #region Index
        // GET: BaseController
        public async Task<ActionResult> Index()
        {
            // Create list to keep models.
            List<TModel> model = new List<TModel>();
            // Open HttpClient instance which will be disposed after 'using' statement
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage result = SetGetHeaders(client, _modelName).Result;
                // If status code is successfull then deserialize JSON to model.
                if (result.IsSuccessStatusCode)
                {
                    string response = result.Content.ReadAsStringAsync().Result;
                    model = JsonConvert.DeserializeObject<List<TModel>>(response);
                }
                return View(model);
            }
        }
        #endregion
        #region Details
        // GET: BaseController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            return View(GetModelById(id).Result);
        }
        #endregion
        #region Create (Return view)
        // GET: BaseController/Create
        public ActionResult Create()
        {
            return View();
        }
        #endregion
        #region Create (Post)
        // POST: BaseController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(TModel model)
        {
            try
            {
                // If all the fields are valid, post the created model and return
                // to the list of models. Otherwise, return the 'Create' view
                if (ModelState.IsValid) {
                    await MakeRequest(model);
                    return RedirectToAction("Index");
                } 
                else return View();
            }
            catch
            {
                return View();
            }
        }
        #endregion
        #region Edit(Return view)
        // GET: BaseController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            return View(GetModelById(id).Result);
        }
        #endregion
        #region Edit (Post)
        // POST: BaseController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, TModel model)
        {
            try
            {
                // If all the fields are valid, put the edited model and return
                // to the list of models. Otherwise, return the 'Edit' view
                if (ModelState.IsValid)
                {
                    await MakeRequest(model, "PUT", id);
                    return RedirectToAction("Index");
                } 
                else return View();
            }
            catch
            {
                return View();
            }
        }
        #endregion
        #region Delete (Return view)
        // GET: BaseController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            return View(GetModelById(id).Result);
        }
        #endregion
        #region Delete (Post)
        // POST: BaseController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, TModel model)
        {
            try
            {
                await MakeRequest(model, "DELETE", id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        #endregion
        #region GetModelById
        public async Task<TModel> GetModelById(int id)
        {
            // Create instance of model.
            TModel model = null;
            // Open HttpClient instance which will be disposed after 'using' statement
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage result = SetGetHeaders(client, _modelName + "/" + id).Result;
                // If status code is successfull then deserialize JSON to model.
                if (result.IsSuccessStatusCode)
                {
                    string response = result.Content.ReadAsStringAsync().Result;
                    model = JsonConvert.DeserializeObject<TModel>(response);
                }
                // Else return model error.
                else
                {
                    ModelState.AddModelError(string.Empty, "Server error! No "
                        + _modelName + " with id " + id);
                }
                return model;
            }
        }
        #endregion
        #region SetGetHeaders
        public async Task<HttpResponseMessage> SetGetHeaders(HttpClient client, string requestUri)
        {
            await SetBaseUri(client);
            // Clear request headers.
            client.DefaultRequestHeaders.Clear();
            // Set request headers.
            client.DefaultRequestHeaders.Accept
                .Add(new MediaTypeWithQualityHeaderValue("application/json"));
            // Get response and store the result in variable.
            return await client.GetAsync(requestUri);
        }
        #endregion
        #region SetBaseUri
        public async Task SetBaseUri(HttpClient client)
        {
            // Set base Uri.
            client.BaseAddress = new System.Uri(BaseUri);
        }
        #endregion
        #region MakeRequest
        public async Task MakeRequest(TModel model, string requestType = "POST", int? id = null)
        {
            using (HttpClient client = new HttpClient())
            {
                // Set base Uri.
                await SetBaseUri(client);
                Task<HttpResponseMessage> postTask;
                if (requestType == "POST")
                {
                    // Post the model.
                    client.PostAsJsonAsync(_modelName, model).Wait();
                }
                else if (requestType == "PUT" && id != null)
                {
                    // Put the edited model
                    client.PutAsJsonAsync(_modelName + "/" + id, model).Wait();
                }
                else if (requestType == "DELETE" && id != null)
                {
                    // Delete the model.
                    client.DeleteAsync(_modelName + "/" + id).Wait();
                }
            }
        }
        #endregion
    }
}
