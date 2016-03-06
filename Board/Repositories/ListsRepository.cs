using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Broad.Models;

namespace Broad.Repositories
{
    public class ListsRepository
    {
        public IEnumerable<List> List()
        {
            List<List> list = new List<List>()
            {
                new List() {Name = "Список1", CreationDate = DateTime.Now, Id = 0},
                new List() {Name = "Список2", CreationDate = DateTime.Now, Id = 1},
                new List() {Name = "Список3", CreationDate = DateTime.Now, Id = 2},
                new List() {Name = "Список4", CreationDate = DateTime.Now, Id = 3},
                new List() {Name = "Список5", CreationDate = DateTime.Now, Id = 4}
            };
            list[0].Cards.Add(new Card()
            {
                Id = 0,
                CreationDate = DateTime.Now,
                PlaneDate = DateTime.Now,
                Name = "Задача по переносу контролов, в админке",
                Level = CardLevel.Danger
            });
            list[0].Cards.Add(new Card()
            {
                Id = 1,
                CreationDate = DateTime.Now,
                PlaneDate = DateTime.Now,
                Name = "Задача2",
                Level = CardLevel.Info
            });
            list[0].Cards.Add(new Card()
            {
                Id = 2,
                CreationDate = DateTime.Now,
                PlaneDate = DateTime.Now,
                Name = "Задача3",
                Level = CardLevel.Warning
            });
            return list;
        }
    }
}