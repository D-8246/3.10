using class_3._10.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace class_3._10.Controllers
{
    public class PeopleController : Controller
    {
       
        public ActionResult Index()
        {
            PeopleManager pm = new PeopleManager(Properties.Settings.Default.ConStr);
            IndexViewModel vm = new IndexViewModel
            {
                People = pm.GetPeople()
            };
            return View(vm);
        }

        public ActionResult AddForm()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddPerson(Person person)
        {
            PeopleManager pm = new PeopleManager(Properties.Settings.Default.ConStr);
            pm.AddPerson(person);         
            return Redirect("/People/Index");
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            PeopleManager pm = new PeopleManager(Properties.Settings.Default.ConStr);
            pm.DeletePerson(id);
            return Redirect("/People/Index");
        }

        public ActionResult Edit(int id)
        {
            PeopleManager pm = new PeopleManager(Properties.Settings.Default.ConStr);
            EditViewModel evm = new EditViewModel
            {
                person = pm.GetPersonById(id)
            };
            return View(evm);
        }

        [HttpPost]
        public ActionResult Update(Person p)
        {
            PeopleManager pm = new PeopleManager(Properties.Settings.Default.ConStr);
            pm.Update(p);
            return Redirect("/People/Index");
        }
    }
}