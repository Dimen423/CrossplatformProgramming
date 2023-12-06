using Lab5.Models;
using Labs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Lab5.Controllers
{
    public class LabsController : Controller
    {
        public IActionResult Lab1()
        {
            if (User.Identity.IsAuthenticated)
            {
                return View();
            }
            else
            {
                return Redirect("/");
            }
        }

        [HttpPost]
        public IActionResult Lab1(Lab1DataModel model)
        {
            var labRunner = new Lab1Manager(model.InitialReputation, model.InputFriendsData.Split("\r\n", StringSplitOptions.TrimEntries).
                Where(row => !string.IsNullOrWhiteSpace(row)).Select(row =>
                {
                    var buf = row.Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
                    return new Tuple<int, int>(buf[0], buf[1]);
                }).ToList());

            try
            {
                model.Calculated = labRunner.Run();
            }
            catch (ArgumentException e)
            {
                model.ErrorValue = e.Message;
            }
            catch (Exception e)
            {
                model.ErrorValue = e.Message;
            }

            return View(model);
        }

        public IActionResult Lab2()
        {
            if (User.Identity.IsAuthenticated)
            {
                return View();
            }
            else
            {
                return Redirect("/");
            }
        }

        [HttpPost]
        public IActionResult Lab2(Lab2DataModel model)
        {
            var labRunner = new Lab2Manager(model.NumberOfTrees);

            try
            {
                model.Calculated = labRunner.Run();
            }
            catch (ArgumentException e)
            {
                model.ErrorValue = e.Message;
            }
            catch (Exception e)
            {
                model.ErrorValue = e.Message;
            }

            return View(model);
        }

        public IActionResult Lab3()
        {
            if (User.Identity.IsAuthenticated)
            {
                return View();
            }
            else
            {
                return Redirect("/");
            }
        }

        [HttpPost]
        public IActionResult Lab3(Lab3DataModel model)
        {
            var connections = ParseConnections(model.Connections);
            var restrictions = model.Restrictions.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList();
            var labRunner = new Lab3Manager(connections, restrictions);
            try
            {
                model.Calculated = labRunner.Run();
            }
            catch (ArgumentException e)
            {
                model.ErrorValue = e.Message;
            }
            catch (Exception e)
            {
                model.ErrorValue = e.Message;
            }

            return View(model);
        }

        private List<Tuple<int, int>> ParseConnections(string connections)
        {
            return connections.Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select(c =>
                {
                    var parts = c.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                    return Tuple.Create(int.Parse(parts[0]), int.Parse(parts[1]));
                }).ToList();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
