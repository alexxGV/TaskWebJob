using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using System.Linq;
using TaskWebJob.Data;
using TaskWebJob.Model;

namespace TaskWebJob.Repositories
{
    public class RepositoryWebJob
    {
        WebJobContext context;
        public RepositoryWebJob(WebJobContext context)
        {
            this.context = context;
        }

        public List<NoticiasRss> GetRess()
        {
            String url = "https://www.chollometro.com/rss";
            XDocument docxml = XDocument.Load(url);
            var consulta = from datos in docxml.Descendants("item")
                           select new NoticiasRss
                           {
                               Title = datos.Element("title").Value,
                               Link = datos.Element("link").Value,
                               Descripcion = datos.Element("description").Value,
                           };
            return consulta.ToList();
        }

        private int GetMaxId()
        {
            if (this.context.WebJobs.Count() == 0)
            {
                return 1;
            }
            else
            {
                return this.context.WebJobs.Max(x => x.IdTitular) + 1;
            }
        }

        public void PopulateDataWebJob()
        {
            List<NoticiasRss> noticias = this.GetRess();
            int id = this.GetMaxId();
            foreach (NoticiasRss rss in noticias)
            {
                WebJob webJob = new WebJob();
                webJob.IdTitular = id;
                webJob.Titulo = rss.Title;
                webJob.Enlace = rss.Link;
                webJob.Descripcion = rss.Descripcion;
                webJob.Fecha = DateTime.Now;
                id += 1;
                this.context.WebJobs.Add(webJob);
            }
            this.context.SaveChanges();
        }
    }
}