using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Hostel.Models;

namespace Hostel.Controllers.HostelControllers
{
    public class PeopleController : Controller
    {
        private HostelContext db = new HostelContext();

        // GET: People
        [Authorize]
        public ActionResult Index()
        {
            var persons = db.Persons.Include(p => p.Room);
            return View(persons.ToList());
        }

        // GET: People/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person person = db.Persons.Find(id);
            if (person == null)
            {
                return HttpNotFound();
            }

            person.Room = db.Rooms?.Find(person.RoomId);
            return View(person);
        }

        // GET: People/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            var roomsAndPersonsCount = GetRoomsAndPersonsCount();
            var freeRooms = roomsAndPersonsCount.Keys.Where(e =>  roomsAndPersonsCount[e] < e.Capacity).ToList();
            ViewBag.RoomId = new SelectList(freeRooms, "RoomId", "Number");
            return View();
        }

        // POST: People/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create([Bind(Include = "PersonId,RoomId,Name")] Person person)
        {
            if (ModelState.IsValid)
            {
                db.Persons.Add(person);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            var roomsAndPersonsCount = GetRoomsAndPersonsCount();
            var freeRooms = roomsAndPersonsCount.Keys.Where(e => roomsAndPersonsCount[e] < e.Capacity).ToList();
            ViewBag.RoomId = new SelectList(freeRooms, "RoomId", "Number", person.RoomId);
            return View(person);
        }

        // GET: People/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person person = db.Persons.Find(id);
            if (person == null)
            {
                return HttpNotFound();
            }

            var roomsAndPersonsCount = GetRoomsAndPersonsCount();
            var freeRooms = roomsAndPersonsCount.Keys.Where(e => roomsAndPersonsCount[e] < e.Capacity).ToList();
            ViewBag.RoomId = new SelectList(freeRooms, "RoomId", "Number", person.RoomId);
            return View(person);
        }

        // POST: People/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "PersonId,RoomId,Name")] Person person)
        {
            if (ModelState.IsValid)
            {
                db.Entry(person).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            var roomsAndPersonsCount = GetRoomsAndPersonsCount();
            var freeRooms = roomsAndPersonsCount.Keys.Where(e => roomsAndPersonsCount[e] < e.Capacity).ToList();
            ViewBag.RoomId = new SelectList(freeRooms, "RoomId", "Number", person.RoomId);
            return View(person);
        }

        // GET: People/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person person = db.Persons.Find(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            person.Room = db.Rooms?.Find(person.RoomId);
            return View(person);
        }

        // POST: People/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            Person person = db.Persons.Find(id);
            db.Persons.Remove(person);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        [Authorize]
        public ActionResult FreeRooms()
        {
            var roomsAndPersonsCount = GetRoomsAndPersonsCount();
            return View(roomsAndPersonsCount);
        }

        private Dictionary<Room, int> GetRoomsAndPersonsCount()
        {
            Dictionary<Room, int> roomsWithPersons = new Dictionary<Room, int>();
            foreach (var room in db.Rooms)
            {
                roomsWithPersons.Add(room, NumberPersonsInRoom(room.RoomId));
            }
            return roomsWithPersons;
        }

        private int NumberPersonsInRoom(int roomId)
        {           
            return db.Persons.Count(c => c.RoomId == roomId);
        }

    }
}
