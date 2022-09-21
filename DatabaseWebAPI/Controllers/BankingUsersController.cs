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
using DatabaseWebAPI.Models;

namespace DatabaseWebAPI.Controllers
{
    public class BankingUsersController : ApiController
    {
        private bankingDBEntities db = new bankingDBEntities();

        // GET: api/BankingUsers
        public IQueryable<BankingUser> GetBankingUsers()
        {
            return db.BankingUsers;
        }

        // GET: api/BankingUsers/5
        [ResponseType(typeof(BankingUser))]
        public IHttpActionResult GetBankingUser(string id)
        {
            BankingUser bankingUser = db.BankingUsers.Find(id);
            if (bankingUser == null)
            {
                return NotFound();
            }

            return Ok(bankingUser);
        }

        // PUT: api/BankingUsers/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutBankingUser(string id, BankingUser bankingUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != bankingUser.accNo)
            {
                return BadRequest();
            }

            db.Entry(bankingUser).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BankingUserExists(id))
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

        // POST: api/BankingUsers
        [ResponseType(typeof(BankingUser))]
        public IHttpActionResult PostBankingUser(BankingUser bankingUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.BankingUsers.Add(bankingUser);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (BankingUserExists(bankingUser.accNo))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = bankingUser.accNo }, bankingUser);
        }

        // DELETE: api/BankingUsers/5
        [ResponseType(typeof(BankingUser))]
        public IHttpActionResult DeleteBankingUser(string id)
        {
            BankingUser bankingUser = db.BankingUsers.Find(id);
            if (bankingUser == null)
            {
                return NotFound();
            }

            db.BankingUsers.Remove(bankingUser);
            db.SaveChanges();

            return Ok(bankingUser);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BankingUserExists(string id)
        {
            return db.BankingUsers.Count(e => e.accNo == id) > 0;
        }
    }
}