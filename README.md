# Contoso
Angular 8 - .Net Core WebAPI Project

Provides base projects for developing production grade application. As requested, simple CRUD operations are performed on Contact screen\table.

UI is based on Angular 8 where are API is based on .Net Core Framework 3.1. Testing is done on Database SQL Db. Contoso db should be pre-created. 

# Contoso UI
Angular 8 with data binding, UI Form group, UI Validations
Please note that Login Screen with JWT Token support provided but not implemented. 

<html>
   <head>
      <link rel='stylesheet' href='https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0-beta.2/css/bootstrap.min.css' integrity='sha384-PsH8R72JQ3SOdhVi3uxftmaW6Vc51MKb0q5P2rRUpPvrszuE4W1povHYgTpBfshb' crossorigin='anonymous'>
      <link rel='stylesheet' href='https://use.fontawesome.com/releases/v5.3.1/css/all.css' integrity='sha384-mzrmE5qonljUremFsqc01SB46JvROS7bZs3IO2EmfFsd15uHvIt+Y8vEf7N7fWAU' crossorigin='anonymous'>
   </head>
   <body>
      <div class='jumbotron'>
         <h1><i class='fab fa-centercode' fa-2x></i>  Contoso Api</h1>
         <h4>v.1.6</h4>
         NET.Core Api Dapper REST service started!<br>appsettings.json configuration:<br>
         <li>Authentication Type: Not Implemented.</li>
         <ul>
            <li>UseMigrationService:True</li>            
            <li>ConnectionStrings:Data Source=localhost;Initial Catalog=Contoso;Trusted_Connection=True;MultipleActiveResultSets=True;</li>
         </ul>
      </div>
      <div class='row'>
         <div class='col-md-3'>
            <h3>API controlers and methods</h3>
            <ul>
               <li>
                  ContactAsync
                  <ul>
                     <li><i>GetAll</i></li>
                     <li><i>GetByFirstName</i></li>
                     <li><i>GetById</i></li>
                     <li><i>Create</i></li>
                     <li><i>Update</i></li>
                     <li><i>Delete</i></li>
                  </ul>
               </li>
               <li>
                  Contact
                  <ul>
                     <li><i>GetAll</i></li>
                     <li><i>GetActiveByFirstName</i></li>
                     <li><i>GetById</i></li>
                     <li><i>Create</i></li>
                     <li><i>Update</i></li>
                     <li><i>Delete</i></li>
                  </ul>
               </li>
               <li>
                  Info
                  <ul>
                     <li><i>get_Configuration</i></li>
                     <li><i>ApiInfo</i></li>
                  </ul>
               </li>
            </ul>
            <p></p>
         </div>
         <div class='col-md-3'>
            <h3>API services and patterns</h3>
            <p>
            <ul>
               <li>Dependency Injection (Net Core feature) </li>
               <li>Repository and Unit of Work Patterns</li>
               <li>Generic services</li>
               <li>Automapper</li>
               <li>Sync and Async calls</li>
               <li>Generic exception handler</li>
               <li>Serilog logging with Console and File sinks</li>
               <li>nUnit Test</li>
            </ul>
         </div>
         <div class='col-md-3'>
            <h3>API projects</h3>
            <ul>
               <li>Api</li>
               <li>Domain</li>
               <li>Entity</li>
               <li>API Test</li>
               <li>Domain Test - Not implemented</li>
            </ul>
         </div>
      </div>
   </body>
</html>
