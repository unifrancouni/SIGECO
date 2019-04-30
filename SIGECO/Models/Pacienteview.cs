// ASP.NET Maker 2019
// Copyright (c) 2019 e.World Technology Limited. All rights reserved.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Dynamic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Internal;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using Ganss.XSS;
using ImageMagick;
using MimeDetective.InMemory;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;
using static AspNetMaker2019.Models.prjSIGECO;

// Models
namespace AspNetMaker2019.Models {

	// Partial class
	public partial class prjSIGECO {

		/// <summary>
		/// Paciente_View
		/// </summary>

		public static _Paciente_View Paciente_View {
			get => HttpData.Get<_Paciente_View>("Paciente_View");
			set => HttpData["Paciente_View"] = value;
		}

		/// <summary>
		/// Page class for Paciente
		/// </summary>

		public class _Paciente_View : _Paciente_ViewBase
		{

			// Construtor
			public _Paciente_View(Controller controller = null) : base(controller) {
			}
		}

		/// <summary>
		/// Page base class
		/// </summary>

		public class _Paciente_ViewBase : _Paciente, IAspNetMakerPage
		{

			// Page ID
			public string PageID = "view";

			// Project ID
			public string ProjectID = "{9B083C8B-EE2F-4356-BE8D-9A26D5707365}";

			// Table name
			public string TableName = "Paciente";

			// Page object name
			public string PageObjName = "Paciente_View";

			// Page headings
			public string Heading = "";
			public string Subheading = "";
			public string PageHeader = "";
			public string PageFooter = "";

			// Token
			public string Token; // DN
			public bool CheckToken = Config.CheckToken;

			// Action result // DN
			public IActionResult ActionResult;

			// Cache // DN
			public IMemoryCache Cache;

			// Page terminated // DN
			private bool _terminated = false;

			// Page URL
			private string _pageUrl = "";

			// Page action result
			public IActionResult PageResult() {
				if (ActionResult != null)
					return ActionResult;
				SetupMenus();
				return Controller.View();
			}

			// Page heading
			public string PageHeading {
				get {
					if (!Empty(Heading))
						return Heading;
					else if (!Empty(Caption))
						return Caption;
					else
						return "";
				}
			}

			// Page subheading
			public string PageSubheading {
				get {
					if (!Empty(Subheading))
						return Subheading;
					if (!Empty(TableName))
						return Language.Phrase(PageID);
					return "";
				}
			}

			// Page name
			public string PageName => CurrentPageName();

			// Page URL
			public string PageUrl {
				get {
					if (_pageUrl == "") {
						_pageUrl = CurrentPageName() + "?";
					}
					return _pageUrl;
				}
			}

			// Export URLs
			public string ExportPrintUrl = "";
			public string ExportHtmlUrl = "";
			public string ExportExcelUrl = "";
			public string ExportWordUrl = "";
			public string ExportXmlUrl = "";
			public string ExportCsvUrl = "";
			public string ExportPdfUrl = "";

			// Update URLs
			public string InlineAddUrl = "";
			public string GridAddUrl = "";
			public string GridEditUrl = "";
			public string MultiDeleteUrl = "";
			public string MultiUpdateUrl = "";

			// Custom export
			public bool ExportExcelCustom = false;
			public bool ExportWordCustom = false;
			public bool ExportPdfCustom = false;
			public bool ExportEmailCustom = false;

			// Private properties
			private string _message = "";
			private string _failureMessage = "";
			private string _successMessage = "";
			private string _warningMessage = "";

			// Message
			public string Message {
				get => Session.TryGetValue(Config.SessionMessage, out string message) ? message : _message;
				set {
					_message = AddMessage(Message, value);
					Session[Config.SessionMessage] = _message;
				}
			}

			// Failure Message
			public string FailureMessage {
				get => Session.TryGetValue(Config.SessionFailureMessage, out string failureMessage) ? failureMessage : _failureMessage;
				set {
					_failureMessage = AddMessage(FailureMessage, value);
					Session[Config.SessionFailureMessage] = _failureMessage;
				}
			}

			// Success Message
			public string SuccessMessage {
				get => Session.TryGetValue(Config.SessionSuccessMessage, out string successMessage) ? successMessage : _successMessage;
				set {
					_successMessage = AddMessage(SuccessMessage, value);
					Session[Config.SessionSuccessMessage] = _successMessage;
				}
			}

			// Warning Message
			public string WarningMessage {
				get => Session.TryGetValue(Config.SessionWarningMessage, out string warningMessage) ? warningMessage : _warningMessage;
				set {
					_warningMessage = AddMessage(WarningMessage, value);
					Session[Config.SessionWarningMessage] = _warningMessage;
				}
			}

			// Clear message
			public void ClearMessage() {
				_message = "";
				Session[Config.SessionMessage] = _message;
			}

			// Clear failure message
			public void ClearFailureMessage() {
				_failureMessage = "";
				Session[Config.SessionFailureMessage] = _failureMessage;
			}

			// Clear success message
			public void ClearSuccessMessage() {
				_successMessage = "";
				Session[Config.SessionSuccessMessage] = _successMessage;
			}

			// Clear warning message
			public void ClearWarningMessage() {
				_warningMessage = "";
				Session[Config.SessionWarningMessage] = _warningMessage;
			}

			// Clear all messages
			public void ClearMessages() {
				ClearMessage();
				ClearFailureMessage();
				ClearSuccessMessage();
				ClearWarningMessage();
			}

			// Get message
			public string GetMessage() { // DN
				bool hidden = false;
				string html = "";

				// Message
				string message = Message;
				Message_Showing(ref message, "");
				if (!Empty(message)) { // Message in Session, display
					if (!hidden)
						message = "<button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"><span aria-hidden=\"true\">&times;</span></button>" + message;
					html += "<div class=\"alert alert-info alert-dismissible ew-info\"><i class=\"icon fa fa-info\"></i>" + message + "</div>";
					Session[Config.SessionMessage] = ""; // Clear message in Session
				}

				// Warning message
				string warningMessage = WarningMessage;
				Message_Showing(ref warningMessage, "warning");
				if (!Empty(warningMessage)) { // Message in Session, display
					if (!hidden)
						warningMessage = "<button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"><span aria-hidden=\"true\">&times;</span></button>" + warningMessage;
					html += "<div class=\"alert alert-warning alert-dismissible ew-warning\"><i class=\"icon fa fa-warning\"></i>" + warningMessage + "</div>";
					Session[Config.SessionWarningMessage] = ""; // Clear message in Session
				}

				// Success message
				string successMessage = SuccessMessage;
				Message_Showing(ref successMessage, "success");
				if (!Empty(successMessage)) { // Message in Session, display
					if (!hidden)
						successMessage = "<button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"><span aria-hidden=\"true\">&times;</span></button>" + successMessage;
					html += "<div class=\"alert alert-success alert-dismissible ew-success\"><i class=\"icon fa fa-check\"></i>" + successMessage + "</div>";
					Session[Config.SessionSuccessMessage] = ""; // Clear message in Session
				}

				// Failure message
				string errorMessage = FailureMessage;
				Message_Showing(ref errorMessage, "failure");
				if (!Empty(errorMessage)) { // Message in Session, display
					if (!hidden)
						errorMessage = "<button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"><span aria-hidden=\"true\">&times;</span></button>" + errorMessage;
					html += "<div class=\"alert alert-danger alert-dismissible ew-error\"><i class=\"icon fa fa-ban\"></i>" + errorMessage + "</div>";
					Session[Config.SessionFailureMessage] = ""; // Clear message in Session
				}
				return "<div class=\"ew-message-dialog\"" + (hidden ? " d-none" : "") + ">" + html + "</div>"; // DN
			}

			// Show message as IHtmlContent // DN
			public IHtmlContent ShowMessages() => new HtmlString(GetMessage());

			// Get messages
			public Dictionary<string, string> GetMessages() {
				var d = new Dictionary<string, string>();

				// Message
				string message = Message;
				if (!Empty(message)) { // Message in Session, display
					d.Add("message", message);
					Session[Config.SessionMessage] = ""; // Clear message in Session
				}

				// Warning message
				string warningMessage = WarningMessage;
				if (!Empty(warningMessage)) { // Message in Session, display
					d.Add("warningMessage", warningMessage);
					Session[Config.SessionWarningMessage] = ""; // Clear message in Session
				}

				// Success message
				string successMessage = SuccessMessage;
				if (!Empty(successMessage)) { // Message in Session, display
					d.Add("successMessage", successMessage);
					Session[Config.SessionSuccessMessage] = ""; // Clear message in Session
				}

				// Failure message
				string failureMessage = FailureMessage;
				if (!Empty(failureMessage)) { // Message in Session, display
					d.Add("failureMessage", failureMessage);
					Session[Config.SessionFailureMessage] = ""; // Clear message in Session
				}
				return d;
			}

			// Show Page Header
			public IHtmlContent ShowPageHeader() {
				string header = PageHeader;
				Page_DataRendering(ref header);
				if (!Empty(header)) // Header exists, display
					return new HtmlString("<p id=\"ew-page-header\">" + header + "</p>");
				return null;
			}

			// Show Page Footer
			public IHtmlContent ShowPageFooter() {
				string footer = PageFooter;
				Page_DataRendered(ref footer);
				if (!Empty(footer)) // Footer exists, display
					return new HtmlString("<p id=\"ew-page-footer\">" + footer + "</p>");
				return null;
			}

			// Validate page request
			public bool IsPageRequest => true;

			// Valid post
			protected async Task<bool> ValidPost() => !CheckToken || !IsPost() || IsApi() || await Antiforgery.IsRequestValidAsync(HttpContext);

			// Create token
			public void CreateToken() {
				Token = Token ?? Antiforgery.GetTokens(HttpContext).RequestToken; // Always create token, required by API file/lookup request
				CurrentToken = Token; // Save to global variable
			}

			// Constructor
			public _Paciente_ViewBase(Controller controller = null) { // DN
				if (controller != null)
					Controller = controller;

				// Initialize
				CurrentPage = this;

				// Language object
				Language = Language ?? new Lang();

				// Table object (Paciente)
				if (Paciente == null || Paciente is _Paciente)
					Paciente = this;

				// DN
				string keyUrl = "";
				string[] keys = new string[0];
				StringValues str = "";
				object obj = null;
				string currentPageName = CurrentPageName();
				if (IsApi()) {
					if (RouteValues.TryGetValue("key", out object k) && !Empty(k))
						keys = k.ToString().Split('/');
					if (keys.Length > 0)
						RecordKeys["nPacienteID"] = keys[0];
				} else {
					RecordKeys["nPacienteID"] = RouteValues.TryGetValue("nPacienteID", out obj) ? Convert.ToString(obj) : Get("nPacienteID");
					keyUrl += "/" + UrlEncode(RecordKeys["nPacienteID"]);
				}
				ExportPrintUrl = currentPageName + keyUrl + "?export=print";
				ExportHtmlUrl = currentPageName + keyUrl + "?export=html";
				ExportExcelUrl = currentPageName + keyUrl + "?export=excel";
				ExportWordUrl = currentPageName + keyUrl + "?export=word";
				ExportXmlUrl = currentPageName + keyUrl + "?export=xml";
				ExportCsvUrl = currentPageName + keyUrl + "?export=csv";
				ExportPdfUrl = currentPageName + keyUrl + "?export=pdf";

				// Table object (Usuario)
				Usuario = Usuario ?? new _Usuario();

				// Start time
				StartTime = Environment.TickCount;

				// Debug message
				LoadDebugMessage();

				// Open connection
				Conn = Connection; // DN

				// User table object (Usuario)
				UserTable = UserTable ?? new _Usuario();
				UserTableConn = UserTableConn ?? GetConnection(UserTable.DbId);

				// Export options
				ExportOptions = new ListOptions { Tag = "div", TagClassName = "ew-export-option" };

				// Other options
				OtherOptions["action"] = new ListOptions { Tag = "div", TagClassName = "ew-action-option" };
				OtherOptions["detail"] = new ListOptions { Tag = "div", TagClassName = "ew-detail-option" };
			}

			#pragma warning disable 1998

			// Export view result
			public async Task<IActionResult> ExportView() { // DN
				if (!Empty(CustomExport) && CustomExport == Export && Config.Export.TryGetValue(CustomExport, out string classname)) {
					IActionResult result = null;
					string content = await GetViewOutput();
					if (Empty(ExportFileName))
						ExportFileName = TableVar;
					dynamic doc = CreateInstance(classname, new object[] { Paciente, "" }); // DN
					doc.Text.Append(content);
					result = doc.Export();
					DeleteTempImages(); // Delete temp images
					return result;
				}
				return null;
			}

			#pragma warning restore 1998

			/// <summary>
			/// Terminate page
			/// </summary>
			/// <param name="url">URL to rediect to</param>
			/// <returns>Page result</returns>

			public IActionResult Terminate(string url = "") { // DN
				if (_terminated) // DN
					return null;

				// Page Unload event
				Page_Unload();

				// Global Page Unloaded event
				Page_Unloaded();
				if (!IsApi())
					Page_Redirecting(ref url);

				// Close connection
				CloseConnections(true);

				// Gargage collection
				Collect(); // DN

				// Terminate
				_terminated = true; // DN

				// Return for API
				if (IsApi()) {
					bool res = !Empty(url);
					if (!res) { // Show error
						var showError = new Dictionary<string, string> { { "success", "false" }, { "version", Config.ProductVersion } };
						foreach (var (key, value) in GetMessages())
							showError.Add(key, value);
						return Controller.Json(showError);
					}
				} else if (ActionResult != null) { // Check action result
					return ActionResult;
				}

				// Go to URL if specified
				if (!Empty(url)) {
					if (!Config.Debug)
						ResponseClear();
					if (!Response.HasStarted) {

						// Handle modal response
						if (IsModal) { // Show as modal
							var row = new Dictionary<string, string> { {"url", GetUrl(url)}, {"modal", "1"} };
							string pageName = GetPageName(url);
							if (pageName != ListUrl) { // Not List page
								row.Add("caption", GetModalCaption(pageName));
								if (pageName == "Pacienteview")
									row.Add("view", "1");
							} else { // List page should not be shown as modal => error
								row.Add("error", FailureMessage);
								ClearFailureMessage();
							}
							return Controller.Json(row);
						} else {
							SaveDebugMessage();
							return Controller.LocalRedirect(AppPath(url));
						}
					}
				}
				return null;
			}

			// Get all records from datareader
			protected async Task<List<Dictionary<string, object>>> GetRecordsFromRecordset(DbDataReader rs)
			{
				var rows = new List<Dictionary<string, object>>();
				while (rs != null && await rs.ReadAsync()) {
					await LoadRowValues(rs); // Set up DbValue/CurrentValue
					rows.Add(GetRecordFromDictionary(GetDictionary(rs)));
				}
				return rows;
			}

			// Get the first record from datareader
			protected async Task<Dictionary<string, object>> GetRecordFromRecordset(DbDataReader rs)
			{
				if (rs != null) {
					await LoadRowValues(rs); // Set up DbValue/CurrentValue
					return GetRecordFromDictionary(GetDictionary(rs));
				}
				return null;
			}

			// Get the first record from the list of records
			protected Dictionary<string, object> GetRecordFromRecordset(List<Dictionary<string, object>> ar) => GetRecordFromDictionary(ar[0]);

			// Get record from Dictionary
			protected Dictionary<string, object> GetRecordFromDictionary(Dictionary<string, object> ar) {
				var row = new Dictionary<string, object>();
				foreach (var (key, value) in ar) {
					if (Fields.TryGetValue(key, out DbField fld)) {
						if (fld.Visible || fld.IsPrimaryKey) { // Primary key or Visible
							if (fld.HtmlTag == "FILE") { // Upload field
								if (Empty(value)) {
									row[key] = null;
								} else {
									if (fld.DataType == Config.DataTypeBlob) {
										string url = FullUrl(GetPageName(Config.ApiUrl) + "/" + Config.ApiFileAction + "/" + fld.TableVar + "/" + fld.Param + "/" + GetRecordKeyValue(ar)); // Query string format
										row[key] = new Dictionary<string, object> { { "mimeType", ContentType((byte[])value) }, { "url", url } };
									} else if (!fld.UploadMultiple || !Convert.ToString(value).Contains(Config.MultipleUploadSeparator)) { // Single file
										row[key] = new Dictionary<string, object> { { "mimeType", ContentType(Convert.ToString(value)) }, { "url", FullUrl(fld.HrefPath + Convert.ToString(value)) } };
									} else { // Multiple files
										var files = Convert.ToString(value).Split(Config.MultipleUploadSeparator);
										row[key] = files.Where(file => !Empty(file)).Select(file => new Dictionary<string, object> { { "type", ContentType(file) }, { "url", FullUrl(fld.HrefPath + file) } });
									}
								}
							} else {
								row[key] = Convert.ToString(value);
							}
						}
					}
				}
				return row;
			}

			// Get record key value from array
			protected string GetRecordKeyValue(Dictionary<string, object> ar) {
				string key = "";
				key += UrlEncode(Convert.ToString(ar["nPacienteID"]));
				return key;
			}

			// Hide fields for Add/Edit
			protected void HideFieldsForAddEdit() {
				if (IsAdd || IsCopy || IsGridAdd)
					nPacienteID.Visible = false;
			}
			public int DisplayRecords = 1; // Number of display records
			public int StartRecord;
			public int StopRecord;
			public int TotalRecords = -1;
			public int RecordRange = 10;
			public int RecordCount;
			public Dictionary<string, string> RecordKeys = new Dictionary<string, string>();
			public bool IsModal = false;
			public string SearchWhere = "";
			public string DbMasterFilter;
			public string DbDetailFilter;
			public bool MasterRecordExists;
			public ListOptions ExportOptions; // Export options
			public ListOptionsDictionary OtherOptions = new ListOptionsDictionary(); // Other options
			public DbDataReader Recordset;

			#pragma warning disable 168, 219

			/// <summary>
			/// Page run
			/// </summary>
			/// <returns>Page result</returns>

			public async Task<IActionResult> Run() {

				// Header
				Header(Config.Cache);

				// Is modal
				IsModal = Param<bool>("modal");

				// User profile
				Profile = new UserProfile();

				// Security
				Security = new AdvancedSecurity(); // DN
				bool validRequest = false;

				// Check security for API request
				if (IsApi() && !Security.IsLoggedIn) {
					var authResult = await HttpContext.AuthenticateAsync(JwtBearerDefaults.AuthenticationScheme);
					if (authResult.Succeeded && authResult.Principal.Identity.IsAuthenticated)
						Security.LoginUser(ClaimValue(ClaimTypes.Name), ClaimValue("userid"), ClaimValue("parentuserid"), ConvertToInt(ClaimValue("userlevelid")));
				}
				if (!validRequest) {
					if (!Security.IsLoggedIn)
						await Security.AutoLogin();
					if (Security.IsLoggedIn)
						Security.TablePermission_Loading();
					Security.LoadCurrentUserLevel(ProjectID + TableName);
					if (Security.IsLoggedIn)
						Security.TablePermission_Loaded();
					if (!Security.CanView) {
						Security.SaveLastUrl();
						FailureMessage = DeniedMessage(); // Set no permission
						if (IsApi())
							return new JsonBoolResult(new { success = false, error = DeniedMessage(), version = Config.ProductVersion }, false);
						if (Security.CanList)
							return Terminate(GetUrl("Pacientelist"));
						else
							return Terminate(GetUrl("login"));
					}
				}

				// Get export parameters
				string custom = "";
				if (!Empty(Param("export"))) {
					Export = Param("export");
					custom = Param("custom");
				} else if (IsPost()) {
					if (!Empty(Post("exporttype")))
						Export = Post("exporttype");
					custom = Post("custom");
				} else if (Get("cmd") == "json") {
					Export = Get("cmd");
				} else {
					ExportReturnUrl = CurrentUrl();
				}
				ExportFileName = TableVar; // Get export file, used in header
				if (!Empty(Get("nPacienteID"))) {
					if (!Empty(ExportFileName))
						ExportFileName += "_";
					ExportFileName += Get("nPacienteID");
				}

				// Get custom export parameters
				if (IsExport() && !Empty(custom)) {
					CustomExport = Export;
					Export = "print";
				}
				CustomExportType = CustomExport;
				ExportType = Export; // Get export parameter, used in header

				// Update Export URLs
				if (Config.UseExcelExtension)
					ExportExcelCustom = false;
				if (ExportExcelCustom)
					ExportExcelUrl += "&amp;custom=1";
				if (ExportWordCustom)
					ExportWordUrl += "&amp;custom=1";
				if (ExportPdfCustom)
					ExportPdfUrl += "&amp;custom=1";
				CurrentAction = Param("action"); // Set up current action

				// Setup export options
				SetupExportOptions();
				nPacienteID.Visible = false;
				nCodigo.SetVisibility();
				sNombre.SetVisibility();
				sApellido1.SetVisibility();
				sApellido2.SetVisibility();
				nGeneroID.SetVisibility();
				sLugarNacimiento.SetVisibility();
				dFechaNacimiento.SetVisibility();
				sDireccion.SetVisibility();
				sCedula.SetVisibility();
				sTelefono.SetVisibility();
				sEmergenciaNombre.SetVisibility();
				sEmergenciaParentesco.SetVisibility();
				sEmergenciaTelefono.SetVisibility();
				nActivo.SetVisibility();
				dFechaCreacion.Visible = false;
				dFechaModificacion.Visible = false;
				HideFieldsForAddEdit();

				// Do not use lookup cache
				SetUseLookupCache(false);

				// Global Page Loading event
				Page_Loading();

				// Page Load event
				Page_Load();

				// Check token
				if (!await ValidPost())
					End(Language.Phrase("InvalidPostRequest"));

				// Create token
				CreateToken();

				// Set up lookup cache
				await SetupLookupOptions(nGeneroID);

				// Check modal
				if (IsModal)
					SkipHeaderFooter = true;

				// Load current record
				bool loadCurrentRecord = false;
				string returnUrl = "";
				bool matchRecord = false;
				if (IsPageRequest) { // Validate request
					string[] keyValues = null;
					object v;
					if (IsApi() && RouteValues.TryGetValue("key", out object k))
						if (!Empty(k))
							keyValues = k.ToString().Split('/');
						else
							return new JsonBoolResult(new { success = false, error = Language.Phrase("NoRecord"), version = Config.ProductVersion }, false);
					if (RouteValues.TryGetValue("nPacienteID", out v) && !Empty(v)) { // DN
						nPacienteID.QueryValue = Convert.ToString(v);
						RecordKeys["nPacienteID"] = nPacienteID.QueryValue;
					} else if (!Empty(Get("nPacienteID"))) {
						nPacienteID.QueryValue = Get("nPacienteID");
						RecordKeys["nPacienteID"] = nPacienteID.QueryValue;
					} else if (IsApi() && !Empty(keyValues)) {
						nPacienteID.QueryValue = Convert.ToString(keyValues[0]);
						RecordKeys["nPacienteID"] = nPacienteID.QueryValue;
					} else {
						returnUrl = "Pacientelist"; // Return to list
					}

					// Get action
					CurrentAction = "show"; // Display form
					switch (CurrentAction) {
						case "show": // Get a record to display
							bool res;
							if (IsApi()) {
								string filter = GetRecordFilter();
								CurrentFilter = filter;
								string sql = CurrentSql;
								var conn = await GetConnectionAsync();
								Recordset = await conn.GetDataReaderAsync(sql);
								res = !Empty(Recordset) && await Recordset.ReadAsync();
							} else {
								res = await LoadRow();
							}
							if (!res) { // Load record based on key
								if (Empty(SuccessMessage) && Empty(FailureMessage))
									FailureMessage = Language.Phrase("NoRecord"); // Set no record message
								if (IsApi()) {
									if (!Empty(SuccessMessage))
										return new JsonBoolResult(new { success = true, message = SuccessMessage, version = Config.ProductVersion }, true);
									else
										return new JsonBoolResult(new { success = false, error = FailureMessage, version = Config.ProductVersion }, false);
								} else {
									return Terminate("Pacientelist"); // Return to list page
								}
							}
							break;
					}
				} else {
					returnUrl = "Pacientelist"; // Not page request, return to list
				}
				if (!Empty(returnUrl))
					return Terminate(returnUrl);

				// Render row
				RowType = Config.RowTypeView;
				ResetAttributes();
				await RenderRow();

				// Export data only
				if (Empty(CustomExport) && Config.Export.ContainsKey(Export)) {
					return await ExportData();
				} else if (!Empty(CustomExport) && CustomExport == Export && Config.Export.ContainsKey(CustomExport)) {
					return await ExportView();
				}

				// Set up Breadcrumb
				if (!IsExport())
					SetupBreadcrumb();

				// Set up detail parameters
				SetupDetailParms();

				// Normal return
				if (IsApi()) {
					var rows = await GetRecordFromRecordset(Recordset); // Get current record only
					return Controller.Json(new Dictionary<string, object> { {"success", true}, {TableVar, rows}, {"version", Config.ProductVersion} });
				}
				return PageResult();
			}

			#pragma warning restore 168, 219

			// Set up other options

			#pragma warning disable 168, 219 

			public void SetupOtherOptions() 
			{
				var options = OtherOptions;
				var option = options["action"];
				ListOption item;
				string links = "";

				// Add
				item = option.Add("add");
				string addcaption = HtmlTitle(Language.Phrase("ViewPageAddLink"));
				if (IsModal) // Modal
					item.Body = "<a class=\"ew-action ew-add\" title=\"" + addcaption + "\" data-caption=\"" + addcaption + "\" href=\"javascript:void(0);\" onclick=\"ew.modalDialogShow({lnk:this,url:'" + HtmlEncode(AppPath(AddUrl)) + "'});\">" + Language.Phrase("ViewPageAddLink") + "</a>";
				else
					item.Body = "<a class=\"ew-action ew-add\" title=\"" + addcaption + "\" data-caption=\"" + addcaption + "\" href=\"" + HtmlEncode(AppPath(AddUrl)) + "\">" + Language.Phrase("ViewPageAddLink") + "</a>";
					item.Visible = (AddUrl != "" && Security.CanAdd);

				// Edit
				item = option.Add("edit");
				var editcaption = HtmlTitle(Language.Phrase("ViewPageEditLink"));
				if (IsModal) // Modal
					item.Body = "<a class=\"ew-action ew-edit\" title=\"" + editcaption + "\" data-caption=\"" + editcaption + "\" href=\"javascript:void(0);\" onclick=\"ew.modalDialogShow({lnk:this,url:'" + HtmlEncode(AppPath(EditUrl)) + "'});\">" + Language.Phrase("ViewPageEditLink") + "</a>";
				else
					item.Body = "<a class=\"ew-action ew-edit\" title=\"" + editcaption + "\" data-caption=\"" + editcaption + "\" href=\"" + HtmlEncode(AppPath(EditUrl)) + "\">" + Language.Phrase("ViewPageEditLink") + "</a>";
					item.Visible = (EditUrl != "" && Security.CanEdit);

				// Copy
				item = option.Add("copy");
				var copycaption = HtmlTitle(Language.Phrase("ViewPageCopyLink"));
				if (IsModal) // Modal
					item.Body = "<a class=\"ew-action ew-copy\" title=\"" + copycaption + "\" data-caption=\"" + copycaption + "\" href=\"javascript:void(0);\" onclick=\"ew.modalDialogShow({lnk:this,url:'" + HtmlEncode(AppPath(CopyUrl)) + "'});\">" + Language.Phrase("ViewPageCopyLink") + "</a>";
				else
					item.Body = "<a class=\"ew-action ew-copy\" title=\"" + copycaption + "\" data-caption=\"" + copycaption + "\" href=\"" + HtmlEncode(AppPath(CopyUrl)) + "\">" + Language.Phrase("ViewPageCopyLink") + "</a>";
					item.Visible = (CopyUrl != "" && Security.CanAdd);

				// Delete
				item = option.Add("delete");
				if (IsModal) // Handle as inline delete
					item.Body = "<a onclick=\"return ew.confirmDelete(this);\" class=\"ew-action ew-delete\" title=\"" + HtmlTitle(Language.Phrase("ViewPageDeleteLink")) + "\" data-caption=\"" + HtmlTitle(Language.Phrase("ViewPageDeleteLink")) + "\" href=\"" + HtmlEncode(UrlAddQuery(AppPath(DeleteUrl), "action=1")) + "\">" + Language.Phrase("ViewPageDeleteLink") + "</a>";
				else
					item.Body = "<a class=\"ew-action ew-delete\" title=\"" + HtmlTitle(Language.Phrase("ViewPageDeleteLink")) + "\" data-caption=\"" + HtmlTitle(Language.Phrase("ViewPageDeleteLink")) + "\" href=\"" + HtmlEncode(AppPath(DeleteUrl)) + "\">" + Language.Phrase("ViewPageDeleteLink") + "</a>";
				item.Visible = (DeleteUrl != "" && Security.CanDelete);
				string body = "";
				option = options["detail"];
				string detailTableLink = "";
				string detailViewTblVar = "";
				string detailCopyTblVar = "";
				string detailEditTblVar = "";

				// "detail_Expediente"
				item = option.Add("detail_Expediente");
				body = Language.Phrase("ViewPageDetailLink") + Language.TablePhrase("Expediente", "TblCaption");
				body = "<a class=\"btn btn-default btn-sm ew-row-link ew-detail\" data-action=\"list\" href=\"" + HtmlEncode(AppPath("Expedientelist?" + Config.TableShowMaster + "=Paciente&fk_nPacienteID=" + UrlEncode(Convert.ToString(nPacienteID.CurrentValue)) + "")) + "\">" + body + "</a>";
				links = "";
				if (Empty(Expediente_Grid))
					Expediente_Grid = new _Expediente_Grid();
				if (Expediente_Grid.DetailView && Security.CanView && Security.AllowView(String.Concat(CurrentProjectID, "Expediente"))) {
					links += "<li><a class=\"ew-row-link ew-detail-view dropdown-item\" data-action=\"view\" data-caption=\"" + HtmlTitle(Language.Phrase("MasterDetailViewLink")) + "\" href=\"" + HtmlEncode(AppPath(GetViewUrl(Config.TableShowDetail + "=Expediente"))) + "\">" + HtmlImageAndText(Language.Phrase("MasterDetailViewLink")) + "</a></li>";
					if (!Empty(detailViewTblVar)) detailViewTblVar += ",";
					detailViewTblVar += "Expediente";
				}
				if (Expediente_Grid.DetailEdit && Security.CanEdit && Security.AllowEdit(String.Concat(CurrentProjectID, "Expediente"))) {
					links += "<li><a class=\"ew-row-link ew-detail-edit dropdown-item\" data-action=\"edit\" data-caption=\"" + HtmlTitle(Language.Phrase("MasterDetailEditLink")) + "\" href=\"" + HtmlEncode(AppPath(GetEditUrl(Config.TableShowDetail + "=Expediente"))) + "\">" + HtmlImageAndText(Language.Phrase("MasterDetailEditLink")) + "</a></li>";
					if (!Empty(detailEditTblVar)) detailEditTblVar += ",";
					detailEditTblVar += "Expediente";
				}
				if (Expediente_Grid.DetailAdd && Security.CanAdd && Security.AllowAdd(String.Concat(CurrentProjectID, "Expediente"))) {
					links += "<li><a class=\"ew-row-link ew-detail-copy dropdown-item\" data-action=\"add\" data-caption=\"" + HtmlTitle(Language.Phrase("MasterDetailCopyLink")) + "\" href=\"" + HtmlEncode(AppPath(GetCopyUrl(Config.TableShowDetail + "=Expediente"))) + "\">" + HtmlImageAndText(Language.Phrase("MasterDetailCopyLink")) + "</a></li>";
					if (!Empty(detailCopyTblVar)) detailCopyTblVar += ",";
					detailCopyTblVar += "Expediente";
				}
				if (!Empty(links)) {
					body += "<button class=\"dropdown-toggle btn btn-default ew-detail\" data-toggle=\"dropdown\"></button>";
					body += "<ul class=\"dropdown-menu\">" + links + "</ul>";
				}
				body = "<div class=\"btn-group btn-group-sm ew-btn-group\">" + body + "</div>";
				item.Body = body;
				item.Visible = Security.AllowList(String.Concat(CurrentProjectID, "Expediente"));
				if (item.Visible) {
					if (!Empty(detailTableLink)) detailTableLink += ",";
					detailTableLink += "Expediente";
				}
				if (ShowMultipleDetails) 
					item.Visible = false;

				// Multiple details
				if (ShowMultipleDetails) {
					body = "<div class=\"btn-group btn-group-sm ew-btn-group\">";
					links = "";
					if (!Empty(detailViewTblVar)) {
						links += "<li><a class=\"ew-row-link ew-detail-view\" data-action=\"view\" data-caption=\"" + HtmlTitle(Language.Phrase("MasterDetailViewLink")) + "\" href=\"" + HtmlEncode(AppPath(GetViewUrl(Config.TableShowDetail + "=" + detailViewTblVar))) + "\">" + HtmlImageAndText(Language.Phrase("MasterDetailViewLink")) + "</a></li>";
					}
					if (!Empty(detailEditTblVar)) {
						links += "<li><a class=\"ew-row-link ew-detail-edit\" data-action=\"edit\" data-caption=\"" + HtmlTitle(Language.Phrase("MasterDetailEditLink")) + "\" href=\"" + HtmlEncode(AppPath(GetEditUrl(Config.TableShowDetail + "=" + detailEditTblVar))) + "\">" + HtmlImageAndText(Language.Phrase("MasterDetailEditLink")) + "</a></li>";
					}
					if (!Empty(detailCopyTblVar)) {
						links += "<li><a class=\"ew-row-link ew-detail-copy\" data-action=\"add\" data-caption=\"" + HtmlTitle(Language.Phrase("MasterDetailCopyLink")) + "\" href=\"" + HtmlEncode(AppPath(GetCopyUrl(Config.TableShowDetail + "=" + detailCopyTblVar))) + "\">" + HtmlImageAndText(Language.Phrase("MasterDetailCopyLink")) + "</a></li>";
					}
					if (!Empty(links)) {
						body += "<button class=\"dropdown-toggle btn btn-default btn-sm ew-master-detail\" title=\"" + HtmlTitle(Language.Phrase("MultipleMasterDetails")) + "\" data-toggle=\"dropdown\">" + Language.Phrase("MultipleMasterDetails") + "</button>";
						body += "<ul class=\"dropdown-menu ew-menu\">" + links + "</ul>";
					}
					body += "</div>";

					// Multiple details
					item = option.Add("details");
					item.Body = body;
				}

				// Set up detail default
				option = options["detail"];
				options["detail"].DropDownButtonPhrase = Language.Phrase("ButtonDetails");
				var ar = detailTableLink.Split(',');
				option.UseDropDownButton = (ar.Length > 1);
				option.UseButtonGroup = true;
				item = option.Add(option.GroupOptionName);
				item.Body = "";
				item.Visible = false;

				// Set up action default
				option = options["action"];
				option.DropDownButtonPhrase = Language.Phrase("ButtonActions");
				option.UseDropDownButton = false;
				option.UseButtonGroup = true;
				item = option.Add(option.GroupOptionName);
				item.Body = "";
				item.Visible = false;
			}

			#pragma warning restore 168, 219

			// Set up starting record parameters
			public void SetupStartRec() {
				int pageNo;

				// Exit if DisplayRecords = 0
				if (DisplayRecords == 0)
					return;
				if (IsPageRequest) { // Validate request

					// Check for a "start" parameter
					if (IsNumeric(Get(Config.TableStartRec))) {
						StartRecord = Get<int>(Config.TableStartRec);
						StartRecordNumber = StartRecord;
					} else if (IsNumeric(Get(Config.TablePageNumber))) {
						pageNo = Get<int>(Config.TablePageNumber);
						StartRecord = (pageNo - 1) * DisplayRecords + 1;
						if (StartRecord <= 0) {
							StartRecord = 1;
						} else if (StartRecord >= ((TotalRecords - 1) / DisplayRecords) * DisplayRecords + 1) {
							StartRecord = ((TotalRecords - 1) / DisplayRecords) * DisplayRecords + 1;
						}
						StartRecordNumber = StartRecord;
					}
				}
				StartRecord = StartRecordNumber;

				// Check if correct start record counter
				if (StartRecord <= 0) { // Avoid invalid start record counter
					StartRecord = 1; // Reset start record counter
					StartRecordNumber = StartRecord;
				} else if (StartRecord > TotalRecords) { // Avoid starting record > total records
					StartRecord = ((TotalRecords - 1) / DisplayRecords) * DisplayRecords + 1; // Point to last page first record
					StartRecordNumber = StartRecord;
				} else if ((StartRecord - 1) % DisplayRecords != 0) {
					StartRecord = ((StartRecord - 1) / DisplayRecords) * DisplayRecords + 1; // Point to page boundary
					StartRecordNumber = StartRecord;
				}
			}

			// Load recordset // DN
			public async Task<DbDataReader> LoadRecordset(int offset = -1, int rowcnt = -1) {

				// Load list page SQL
				string sql = ListSql;

				// Load recordset (Recordset_Selected event not supported) // DN
				return await Connection.SelectLimit(sql, rowcnt, offset, !Empty(OrderBy) || !Empty(SessionOrderBy));
			}

			// Load row based on key values
			public async Task<bool> LoadRow() {
				string filter = GetRecordFilter();

				// Call Row Selecting event
				Row_Selecting(ref filter);

				// Load SQL based on filter
				CurrentFilter = filter;
				string sql = CurrentSql;
				bool res = false;
				try {
					using (var rsrow = await Connection.OpenDataReaderAsync(sql)) {
						if (rsrow != null && await rsrow.ReadAsync()) {
							await LoadRowValues(rsrow);
							res = true;
						} else {
							return false;
						}
					}
				} catch {
					if (Config.Debug)
						throw;
				}
				return res;
			}

			#pragma warning disable 162, 168, 1998

			// Load row values from recordset
			public async Task LoadRowValues(DbDataReader dr = null) {
				Dictionary<string, object> row;
				object v;
				if (dr != null && dr.HasRows)
					row = Connection.GetRow(dr); // DN
				else
					row = NewRow();

				// Call Row Selected event
				Row_Selected(row);
				if (dr == null || !dr.HasRows)
					return;
				nPacienteID.SetDbValue(row["nPacienteID"]);
				nCodigo.SetDbValue(row["nCodigo"]);
				sNombre.SetDbValue(row["sNombre"]);
				sApellido1.SetDbValue(row["sApellido1"]);
				sApellido2.SetDbValue(row["sApellido2"]);
				nGeneroID.SetDbValue(row["nGeneroID"]);
				sLugarNacimiento.SetDbValue(row["sLugarNacimiento"]);
				dFechaNacimiento.SetDbValue(row["dFechaNacimiento"]);
				sDireccion.SetDbValue(row["sDireccion"]);
				sCedula.SetDbValue(row["sCedula"]);
				sTelefono.SetDbValue(row["sTelefono"]);
				sEmergenciaNombre.SetDbValue(row["sEmergenciaNombre"]);
				sEmergenciaParentesco.SetDbValue(row["sEmergenciaParentesco"]);
				sEmergenciaTelefono.SetDbValue(row["sEmergenciaTelefono"]);
				nActivo.SetDbValue((ConvertToBool(row["nActivo"]) ? "1" : "0"));
				dFechaCreacion.SetDbValue(row["dFechaCreacion"]);
				dFechaModificacion.SetDbValue(row["dFechaModificacion"]);
			}

			#pragma warning restore 162, 168, 1998

			// Return a row with default values
			protected Dictionary<string, object> NewRow() {
				var row = new Dictionary<string, object>();
				row.Add("nPacienteID", System.DBNull.Value);
				row.Add("nCodigo", System.DBNull.Value);
				row.Add("sNombre", System.DBNull.Value);
				row.Add("sApellido1", System.DBNull.Value);
				row.Add("sApellido2", System.DBNull.Value);
				row.Add("nGeneroID", System.DBNull.Value);
				row.Add("sLugarNacimiento", System.DBNull.Value);
				row.Add("dFechaNacimiento", System.DBNull.Value);
				row.Add("sDireccion", System.DBNull.Value);
				row.Add("sCedula", System.DBNull.Value);
				row.Add("sTelefono", System.DBNull.Value);
				row.Add("sEmergenciaNombre", System.DBNull.Value);
				row.Add("sEmergenciaParentesco", System.DBNull.Value);
				row.Add("sEmergenciaTelefono", System.DBNull.Value);
				row.Add("nActivo", System.DBNull.Value);
				row.Add("dFechaCreacion", System.DBNull.Value);
				row.Add("dFechaModificacion", System.DBNull.Value);
				return row;
			}

			#pragma warning disable 1998

			// Render row values based on field settings
			public async Task RenderRow() {
				SetupOtherOptions();

				// Call Row_Rendering event
				Row_Rendering();

				// Common render codes for all row types
				// nPacienteID
				// nCodigo
				// sNombre
				// sApellido1
				// sApellido2
				// nGeneroID
				// sLugarNacimiento
				// dFechaNacimiento
				// sDireccion
				// sCedula
				// sTelefono
				// sEmergenciaNombre
				// sEmergenciaParentesco
				// sEmergenciaTelefono
				// nActivo
				// dFechaCreacion
				// dFechaModificacion

				if (RowType == Config.RowTypeView) { // View row

					// nCodigo
					nCodigo.ViewValue = nCodigo.CurrentValue;
					nCodigo.ViewValue = FormatNumber(nCodigo.ViewValue, 0, -2, -2, -2);

					// sNombre
					sNombre.ViewValue = sNombre.CurrentValue;

					// sApellido1
					sApellido1.ViewValue = sApellido1.CurrentValue;

					// sApellido2
					sApellido2.ViewValue = sApellido2.CurrentValue;

					// nGeneroID
					curVal = Convert.ToString(nGeneroID.CurrentValue);
					if (!Empty(curVal)) {
						nGeneroID.ViewValue = nGeneroID.LookupCacheOption(curVal);
						if (nGeneroID.ViewValue == null) { // Lookup from database
							filterWrk = "[nValorCatalogoID]" + SearchString("=", curVal.Trim(), Config.DataTypeNumber, "");
							lookupFilter = () => "nCatalogoID=7";
							sqlWrk = nGeneroID.Lookup.GetSql(false, filterWrk, lookupFilter, this);
							rswrk = await Connection.GetRowsAsync(sqlWrk);
							if (rswrk != null && rswrk.Count > 0) { // Lookup values found
								var listwrk = rswrk[0].Values.ToList();
								listwrk[1] = Convert.ToString(FormatNumber(listwrk[1], 0, -2, -2, -2));
								listwrk[2] = Convert.ToString(listwrk[2]);
								nGeneroID.ViewValue = nGeneroID.DisplayValue(listwrk);
							} else {
								nGeneroID.ViewValue = nGeneroID.CurrentValue;
							}
						}
					} else {
						nGeneroID.ViewValue = System.DBNull.Value;
					}

					// sLugarNacimiento
					sLugarNacimiento.ViewValue = sLugarNacimiento.CurrentValue;

					// dFechaNacimiento
					dFechaNacimiento.ViewValue = dFechaNacimiento.CurrentValue;
					dFechaNacimiento.ViewValue = FormatDateTime(dFechaNacimiento.ViewValue, 0);

					// sDireccion
					sDireccion.ViewValue = sDireccion.CurrentValue;

					// sCedula
					sCedula.ViewValue = sCedula.CurrentValue;

					// sTelefono
					sTelefono.ViewValue = sTelefono.CurrentValue;

					// sEmergenciaNombre
					sEmergenciaNombre.ViewValue = sEmergenciaNombre.CurrentValue;

					// sEmergenciaParentesco
					sEmergenciaParentesco.ViewValue = sEmergenciaParentesco.CurrentValue;

					// sEmergenciaTelefono
					sEmergenciaTelefono.ViewValue = sEmergenciaTelefono.CurrentValue;

					// nActivo
					if (ConvertToBool(nActivo.CurrentValue)) {
						nActivo.ViewValue = (nActivo.TagCaption(1) != "") ? nActivo.TagCaption(1) : "Yes";
					} else {
						nActivo.ViewValue = (nActivo.TagCaption(2) != "") ? nActivo.TagCaption(2) : "No";
					}

					// nCodigo
					nCodigo.HrefValue = "";
					nCodigo.TooltipValue = "";

					// sNombre
					sNombre.HrefValue = "";
					sNombre.TooltipValue = "";

					// sApellido1
					sApellido1.HrefValue = "";
					sApellido1.TooltipValue = "";

					// sApellido2
					sApellido2.HrefValue = "";
					sApellido2.TooltipValue = "";

					// nGeneroID
					nGeneroID.HrefValue = "";
					nGeneroID.TooltipValue = "";

					// sLugarNacimiento
					sLugarNacimiento.HrefValue = "";
					sLugarNacimiento.TooltipValue = "";

					// dFechaNacimiento
					dFechaNacimiento.HrefValue = "";
					dFechaNacimiento.TooltipValue = "";

					// sDireccion
					sDireccion.HrefValue = "";
					sDireccion.TooltipValue = "";

					// sCedula
					sCedula.HrefValue = "";
					sCedula.TooltipValue = "";

					// sTelefono
					sTelefono.HrefValue = "";
					sTelefono.TooltipValue = "";

					// sEmergenciaNombre
					sEmergenciaNombre.HrefValue = "";
					sEmergenciaNombre.TooltipValue = "";

					// sEmergenciaParentesco
					sEmergenciaParentesco.HrefValue = "";
					sEmergenciaParentesco.TooltipValue = "";

					// sEmergenciaTelefono
					sEmergenciaTelefono.HrefValue = "";
					sEmergenciaTelefono.TooltipValue = "";

					// nActivo
					nActivo.HrefValue = "";
					nActivo.TooltipValue = "";
				}

				// Call Row Rendered event
				if (RowType != Config.RowTypeAggregateInit)
					Row_Rendered();
			}

			#pragma warning restore 1998

			// Save data to memory cache
			public void SetCache<T>(string key, T value, int span) => Cache.Set<T>(key, value, new MemoryCacheEntryOptions()
				.SetSlidingExpiration(TimeSpan.FromMilliseconds(span))); // Keep in cache for this time, reset time if accessed

			// Gete data from memory cache
			public void GetCache<T>(string key) => Cache.Get<T>(key);

			// Get export HTML tag
			protected string GetExportTag(string type, bool custom = false) {
				if (SameText(type, "excel")) {
					if (custom)
						return "<a href=\"javascript:void(0);\" class=\"ew-export-link ew-excel\" title=\"" + HtmlEncode(Language.Phrase("ExportToExcelText")) + "\" data-caption=\"" + HtmlEncode(Language.Phrase("ExportToExcelText")) + "\" onclick=\"ew.export(document.fPacienteview,'" + AppPath(ExportExcelUrl) + "','excel',true);\">" + Language.Phrase("ExportToExcel") + "</a>";
					else
						return "<a href=\"" + AppPath(ExportExcelUrl) + "\" class=\"ew-export-link ew-excel\" title=\"" + HtmlEncode(Language.Phrase("ExportToExcelText")) + "\" data-caption=\"" + HtmlEncode(Language.Phrase("ExportToExcelText")) + "\">" + Language.Phrase("ExportToExcel") + "</a>";
				} else if (SameText(type, "word")) {
					if (custom)
						return "<a href=\"javascript:void(0);\" class=\"ew-export-link ew-word\" title=\"" + HtmlEncode(Language.Phrase("ExportToWordText")) + "\" data-caption=\"" + HtmlEncode(Language.Phrase("ExportToWordText")) + "\" onclick=\"ew.export(document.fPacienteview,'" + AppPath(ExportWordUrl) + "','word',true);\">" + Language.Phrase("ExportToWord") + "</a>";
					else
						return "<a href=\"" + AppPath(ExportWordUrl) + "\" class=\"ew-export-link ew-word\" title=\"" + HtmlEncode(Language.Phrase("ExportToWordText")) + "\" data-caption=\"" + HtmlEncode(Language.Phrase("ExportToWordText")) + "\">" + Language.Phrase("ExportToWord") + "</a>";
				} else if (SameText(type, "pdf")) {
					if (custom)
						return "<a href=\"javascript:void(0);\" class=\"ew-export-link ew-pdf\" title=\"" + HtmlEncode(Language.Phrase("ExportToPDFText")) + "\" data-caption=\"" + HtmlEncode(Language.Phrase("ExportToPDFText")) + "\" onclick=\"ew.export(document.fPacienteview,'" + AppPath(ExportPdfUrl) + "','pdf',true);\">" + Language.Phrase("ExportToPDF") + "</a>";
					else
						return "<a href=\"" + AppPath(ExportPdfUrl) + "\" class=\"ew-export-link ew-pdf\" title=\"" + HtmlEncode(Language.Phrase("ExportToPDFText")) + "\" data-caption=\"" + HtmlEncode(Language.Phrase("ExportToPDFText")) + "\">" + Language.Phrase("ExportToPDF") + "</a>";
				} else if (SameText(type, "html")) {
					return "<a href=\"" + AppPath(ExportHtmlUrl) + "\" class=\"ew-export-link ew-html\" title=\"" + HtmlEncode(Language.Phrase("ExportToHtmlText")) + "\" data-caption=\"" + HtmlEncode(Language.Phrase("ExportToHtmlText")) + "\">" + Language.Phrase("ExportToHtml") + "</a>";
				} else if (SameText(type, "xml")) {
					return "<a href=\"" + AppPath(ExportXmlUrl) + "\" class=\"ew-export-link ew-xml\" title=\"" + HtmlEncode(Language.Phrase("ExportToXmlText")) + "\" data-caption=\"" + HtmlEncode(Language.Phrase("ExportToXmlText")) + "\">" + Language.Phrase("ExportToXml") + "</a>";
				} else if (SameText(type, "csv")) {
					return "<a href=\"" + AppPath(ExportCsvUrl) + "\" class=\"ew-export-link ew-csv\" title=\"" + HtmlEncode(Language.Phrase("ExportToCsvText")) + "\" data-caption=\"" + HtmlEncode(Language.Phrase("ExportToCsvText")) + "\">" + Language.Phrase("ExportToCsv") + "</a>";
				} else if (SameText(type, "print")) {
					return "<a href=\"" + AppPath(ExportPrintUrl) + "\" class=\"ew-export-link ew-print\" title=\"" + HtmlEncode(Language.Phrase("PrinterFriendlyText")) + "\" data-caption=\"" + HtmlEncode(Language.Phrase("PrinterFriendlyText")) + "\">" + Language.Phrase("PrinterFriendly") + "</a>";
				}
				return "";
			}

			// Set up export options
			protected void SetupExportOptions() {
				ListOption item;

				// Printer friendly
				item = ExportOptions.Add("print");
				item.Body = GetExportTag("print");
				item.Visible = true;

				// Export to Excel
				item = ExportOptions.Add("excel");
				item.Body = GetExportTag("excel");
				item.Visible = true;

				// Export to Word
				item = ExportOptions.Add("word");
				item.Body = GetExportTag("word");
				item.Visible = false;

				// Export to HTML
				item = ExportOptions.Add("html");
				item.Body = GetExportTag("html");
				item.Visible = false;

				// Export to XML
				item = ExportOptions.Add("xml");
				item.Body = GetExportTag("xml");
				item.Visible = false;

				// Export to CSV
				item = ExportOptions.Add("csv");
				item.Body = GetExportTag("csv");
				item.Visible = true;

				// Export to PDF
				item = ExportOptions.Add("pdf");
				item.Body = GetExportTag("pdf");
				item.Visible = true;

				// Export to Email
				item = ExportOptions.Add("email");
				var url = "";
				item.Body = "<button id=\"emf_Paciente\" class=\"ew-export-link ew-email\" title=\"" + Language.Phrase("ExportToEmailText") + "\" data-caption=\"" + Language.Phrase("ExportToEmailText") + "\" onclick=\"ew.emailDialogShow({lnk:'emf_Paciente',hdr:ew.language.phrase('ExportToEmailText'),f:document.fPacienteview,key:" + ConvertToJsonAttribute(RecordKeys) + ",sel:false" + url + "});\">" + Language.Phrase("ExportToEmail") + "</button>";
				item.Visible = false;

				// Drop down button for export
				ExportOptions.UseButtonGroup = true;
				ExportOptions.UseDropDownButton = false;
				if (ExportOptions.UseButtonGroup && IsMobile())
					ExportOptions.UseDropDownButton = true;
				ExportOptions.DropDownButtonPhrase = Language.Phrase("ButtonExport");

				// Add group option item
				item = ExportOptions.Add(ExportOptions.GroupOptionName);
				item.Body = "";
				item.Visible = false;

				// Hide options for export
				if (IsExport())
					ExportOptions.HideAllOptions();
			}

			#pragma warning disable 168

			/// <summary>
			/// Export data
			/// </summary>

			public async Task<IActionResult> ExportData() {
				var selectLimit = false; // View page

				// Load recordset // DN
				DbDataReader rs = null;
				if (!selectLimit) { // View page
					if (Recordset == null) {
						Recordset = await LoadRecordset();
						await Recordset.ReadAsync(); // Move to the start record
					}
					rs = Recordset;
				}
				if (TotalRecords < 0)
					TotalRecords = await ListRecordCount();
				StartRecord = 1;
				SetupStartRec(); // Set up start record position

				// Set the last record to display
				if (DisplayRecords < 0) {
					StopRecord = TotalRecords;
				} else {
					StopRecord = StartRecord + DisplayRecords - 1;
				}
				var doc = CreateExportDocument(this, "v");
				ExportDoc = doc;
				if (doc == null) { // DN
					FailureMessage = Language.Phrase("ExportClassNotFound"); // Export class not found
					return Controller.Content(GetMessage(), "text/html");
				}

				// Call Page Exporting server event
				ExportDoc.ExportCustom = !Page_Exporting();
				string exportStyle;

				// Page header
				string header = PageHeader;
				Page_DataRendering(ref header);
				doc.Text.Append(header);

				// Export
				await ExportDocument(doc, rs, StartRecord, StopRecord, "view");

				// Export detail records (Expediente)
				if (Config.ExportDetailRecords && CurrentDetailTables.Contains("Expediente")) {
					using (var c = await CreateConnectionAsync(Expediente.DbId)) { // Note: Use new connection for detail records // DN
						using (var rsdetail = await Expediente.LoadRs(Expediente.DetailFilter, c)) { // Load detail records
							if (rsdetail != null && rsdetail.HasRows) { // DN
								exportStyle = doc.Style;
								doc.SetStyle("h"); // Change to horizontal
								if (Export != "csv" || Config.ExportDetailRecordsForCsv) {
									doc.ExportEmptyRow();
									int detailcnt = await Expediente.LoadRecordCount(Expediente.DetailFilter); // DN
									var oldtbl = doc.Table;
									doc.Table = Expediente;
									await Expediente.ExportDocument(doc, rsdetail, 1, detailcnt);
									doc.Table = oldtbl;
								}
								doc.SetStyle(exportStyle); // Restore
							}
						}
					}
				}

				// Page footer
				string footer = PageFooter;
				Page_DataRendered(ref footer);
				doc.Text.Append(footer);

				// Close recordset
				using (rs) {} // Dispose

				// Call Page Exported server event
				Page_Exported();

				// Export header and footer
				await doc.ExportHeaderAndFooter();

				// Log debug message if enabled // DN
				if (Config.Debug)
					Log(GetDebugMessage());

				// Clean output buffer
				Response.Clear();

				// Output data
				if (IsExport("email")) {

					// Export-to-email disabled
					return null;
				}
				return doc.Export();
			}

			#pragma warning restore 168

			// Set up detail parms based on QueryString
			protected void SetupDetailParms() {
				StringValues detailTblVar = "";

				// Get the keys for master table
				if (Query.TryGetValue(Config.TableShowDetail, out detailTblVar)) { // Value may be empty
					CurrentDetailTable = detailTblVar;
				} else {
					detailTblVar = CurrentDetailTable;
				}
				if (!Empty(detailTblVar)) {
					var detailTblVars = new List<string>(detailTblVar.ToString().Split(','));
					if (detailTblVars.Contains("Expediente")) {
						Expediente_Grid = Expediente_Grid ?? new _Expediente_Grid();
						if (Expediente_Grid.DetailView) {
							Expediente_Grid.CurrentMode = "view";

							// Save current master table to detail table
							Expediente_Grid.CurrentMasterTable = TableVar;
							Expediente_Grid.StartRecordNumber = 1;
							Expediente_Grid.nPacienteID.IsDetailKey = true;
							Expediente_Grid.nPacienteID.CurrentValue = nPacienteID.CurrentValue;
							Expediente_Grid.nPacienteID.SessionValue = Expediente_Grid.nPacienteID.CurrentValue;
						}
					}
				}
			}

			// Set up Breadcrumb
			protected void SetupBreadcrumb() {
				var breadcrumb = new Breadcrumb();
				string url = CurrentUrl();
				breadcrumb.Add("list", TableVar, AppPath(AddMasterUrl("Pacientelist")), "", TableVar, true);
				string pageId = "view";
				breadcrumb.Add("view", pageId, url);
				CurrentBreadcrumb = breadcrumb;
			}

			// Setup lookup options
			public async Task SetupLookupOptions(DbField fld)
			{
				Func<string> lookupFilter = null;
				if (!Empty(fld.Lookup) && fld.Lookup.Options.Count == 0) {

					// Set up lookup SQL
					switch (fld.FieldVar) {
						case "x_nGeneroID":
							lookupFilter = () => "nCatalogoID=7";
							break;
						default:
							break;
					}

					// Always call to Lookup.GetSql so that user can setup Lookup.Options in Lookup_Selecting server event
					var sql = fld.Lookup.GetSql(false, "", lookupFilter, this);

					// Set up lookup cache
					if (fld.UseLookupCache && !Empty(sql) && fld.Lookup.ParentFields.Count == 0 && fld.Lookup.Options.Count == 0) {
						int totalCnt = await TryGetRecordCount(sql);
						if (totalCnt > fld.LookupCacheCount) // Total count > cache count, do not cache
							return;
						var ar = new Dictionary<string, Dictionary<string, object>>();
						var values = new List<object>();
						var conn = await GetConnectionAsync();
						List<Dictionary<string, object>> rs = await conn.GetRowsAsync(sql);
						if (rs != null) {
							foreach (var row in rs) {

								// Format the field values
								switch (fld.FieldVar) {
									case "x_nGeneroID":

										//row[1] = FormatNumber(row[1], 0, -2, -2, -2);
										//row["df"] = row[1];

										values = row.Values.ToList();
										row["df"] = FormatNumber(values[1], 0, -2, -2, -2);
									break;
								}
								if (!ar.ContainsKey(row.Values.First().ToString()))
									ar.Add(row.Values.First().ToString(), row);
							}
						}
						fld.Lookup.Options = ar;
					}
				}
			}

			// Close recordset
			public void CloseRecordset() {
				using (Recordset) {} // Dispose
			}

			// Page Load event
			public virtual void Page_Load() {

				//Log("Page Load");
			}

			// Page Unload event
			public virtual void Page_Unload() {

				//Log("Page Unload");
			}

			// Page Redirecting event
			public virtual void Page_Redirecting(ref string url) {

				//url = newurl;
			}

			// Message Showing event
			// type = ""|"success"|"failure"|"warning"

			public virtual void Message_Showing(ref string msg, string type) {

				// Note: Do not change msg outside the following 4 cases.
				if (type == "success") {

					//msg = "your success message";
				} else if (type == "failure") {

					//msg = "your failure message";
				} else if (type == "warning") {

					//msg = "your warning message";
				} else {

					//msg = "your message";
				}
			}

			// Page Load event
			public virtual void Page_Render() {

				//Log("Page Render");
			}

			// Page Data Rendering event
			public virtual void Page_DataRendering(ref string header) {

				// Example:
				//header = "your header";

			}

			// Page Data Rendered event
			public virtual void Page_DataRendered(ref string footer) {

				// Example:
				//footer = "your footer";

			}
		}
	}
}
