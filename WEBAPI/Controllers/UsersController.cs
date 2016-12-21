using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WEBAPI;
using WEBAPI.Models;
using System.Threading.Tasks;

namespace WEBAPI.Controllers
{
    public class UsersController : ApiController
    {
        private usersEntities1 db = new usersEntities1();

        private List<Users> getUsersData() {
            List<Users> userlist = new List<Users>() {
               new Users() { id=123345,name="pablo",age="23",isAnonimus=false},
               new Users() { id=124545,name="pablo",age="11",isAnonimus=false},
               new Users() { id=127845,name="pablo",age="41",isAnonimus=false},
               new Users() { id=123145,name="pablo",age="21",isAnonimus=false},
               new Users() { id=129245,name="pablo",age="51",isAnonimus=false},
               new Users() { id=129945,name="pablo",age="12",isAnonimus=false},
               new Users() { id=122245,name="pablo",age="19",isAnonimus=false } ,
            };
            return userlist;

        }

        // GET: api/Users
        public dynamic Getaspnet_Users()
        {
            return new { users = getUsersData() };
        }

        // GET: api/Users/5
        [ResponseType(typeof(aspnet_Users))]
        public dynamic Getaspnet_Users(int id)
        {
            // aspnet_Users aspnet_Users = db.aspnet_Users.Find(id);
            var user = getUsersData().Find(x => x.id == id);

            if (user == null)
            {
                return new { error = NotFound()};
            }

            return new {user=user ,comments = getUsersData() };
        }

        // PUT: api/Users/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putaspnet_Users(Guid id, aspnet_Users aspnet_Users)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != aspnet_Users.UserId)
            {
                return BadRequest();
            }

            db.Entry(aspnet_Users).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!aspnet_UsersExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Users
        [ResponseType(typeof(aspnet_Users))]
        public IHttpActionResult Postaspnet_Users(aspnet_Users aspnet_Users)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.aspnet_Users.Add(aspnet_Users);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (aspnet_UsersExists(aspnet_Users.UserId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = aspnet_Users.UserId }, aspnet_Users);
        }

        // DELETE: api/Users/5
        [ResponseType(typeof(aspnet_Users))]
        public IHttpActionResult Deleteaspnet_Users(Guid id)
        {
            aspnet_Users aspnet_Users = db.aspnet_Users.Find(id);
            if (aspnet_Users == null)
            {
                return NotFound();
            }

            db.aspnet_Users.Remove(aspnet_Users);
            db.SaveChanges();

            return Ok(aspnet_Users);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool aspnet_UsersExists(Guid id)
        {
            return db.aspnet_Users.Count(e => e.UserId == id) > 0;
        }
    }
}