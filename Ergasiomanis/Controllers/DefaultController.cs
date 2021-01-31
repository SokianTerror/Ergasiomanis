using Ergasiomanis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ergasiomanis.Controllers
{
    public class DefaultController : Controller
    {
        private pubsEntities db = new pubsEntities();

        // GET: Default
        public ActionResult Index()
        {
            string quer = "select authors.au_id, authors.au_fname, authors.au_lname, sum(sales.qty) as ola"
                +"from authors, titleauthor, sales"
                +"where authors.au_id = titleauthor.au_id and titleauthor.title_id = sales.title_id"
                +"group by  authors.au_id, authors.au_fname, authors.au_lname"
                +"order by ola";
            var lista = db.authors.SqlQuery(quer);
            return View(lista);
        }
    }
}