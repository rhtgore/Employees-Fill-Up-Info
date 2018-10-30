using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebEmployeeDataAccess;

namespace WebEmployees.Controllers
{
    public class NewEmployeeController : ApiController
    {
        public IEnumerable<Employee> Get()
        {
            using (NewEmployeeDBEntities entities = new NewEmployeeDBEntities())
            {
                return entities.Employees.ToList();
            }
        }

        public HttpResponseMessage Get(int id)
        {
            using (NewEmployeeDBEntities entities = new NewEmployeeDBEntities())
            {
             
                var entity = entities.Employees.FirstOrDefault(e => e.ID == id);
                if(entity!=null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, entity);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Employee with Id=" +id.ToString()+ "NOt Found"); 
                }
            }
        }

        public HttpResponseMessage Post([FromBody]Employee emp)
        {
            try
            {
                using (NewEmployeeDBEntities entities = new NewEmployeeDBEntities())
                {
                    entities.Employees.Add(emp);
                    entities.SaveChanges();

                     var message = Request.CreateResponse(HttpStatusCode.Created, emp);
                     message.Headers.Location = new Uri(Request.RequestUri + emp.ID.ToString());
                     return message;
                 }
             } 
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest,ex);
            }
        }

        public HttpResponseMessage Delete(int id )
        {
            try
            {
                using (NewEmployeeDBEntities entities = new NewEmployeeDBEntities())
                {
                    var entity = entities.Employees.FirstOrDefault(e => e.ID == id);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Employee with ID" + id.ToString() + "not Found");
                    }
                    else
                    {
                        entities.Employees.Remove(entity);
                        entities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK);
                    }

                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest,ex);
            }
        }

        public HttpResponseMessage Put(int id, [FromBody]Employee emp)
        {
            try
            {
                using (NewEmployeeDBEntities entities = new NewEmployeeDBEntities())
                {
                    var entity = entities.Employees.FirstOrDefault(e => e.ID == id);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Employee with ID=" + id.ToString() + "NOt Found");
                    }
                    else
                    {
                        entity.Username = emp.Username;
                        entity.Password = emp.Password;
                        entity.FirstName = emp.FirstName;
                        entity.LastName = emp.LastName;
                        entity.Department = emp.Department;

                        entities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK, entity);
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest,ex);
            }
        }
    }
}
