﻿using Microsoft.AspNetCore.Mvc;
using SOSRequestPortal.Models;
using SOSRequestPortal.Repository;
using System;

namespace SOSRequestPortal.Controllers
{
	public class UserController : Controller
    {
		public RequestRepository requestRepository = RequestRepository.GetInstance();

		// GET: User
		public ActionResult Index()
        {
            return View(requestRepository.requests);
        }

		// GET: User/Details/5
		public ActionResult Details(int id)
		{
			return View();
		}

		public ActionResult Login()
		{
			return View();
		}

		// GET: User/Create
		public ActionResult Criar()
        {
            return View();
        }

        // POST: User/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Criar(Request collection)
        {
            try
            {
				TimeZoneInfo hrBrasilia = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");
				collection.RequestID = requestRepository.requests.Count;
				collection.Data = DateTime.Now.ToString("dd/MM/yyyy");
				collection.Colaborador = "API AMIGO";
				collection.Time = "API AMIGO";
				collection.Gestor = "API AMIGO";
				collection.Requisitante = "API AMIGO";
				collection.HorarioDaRequisicao = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now, hrBrasilia).ToString("HH:mm:ss");
				requestRepository.requests.Add(collection);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: User/Edit/5
        public ActionResult Editar(int id)
        {
			var request = requestRepository.requests.Find(r => r.RequestID == id);
            return View(request);
		}

        // POST: User/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(int id, Request collection)
        {
            try
            {
				var index = requestRepository.requests.FindIndex(r => r.RequestID == id);
				requestRepository.requests[index] = collection;

				return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: User/Delete/5
        public ActionResult Deletar(int id)
        {
			var request = requestRepository.requests.Find(r => r.RequestID == id);
			return View(request);
        }

        // POST: User/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Deletar(int id, Request collection)
        {
            try
            {
				requestRepository.requests.RemoveAll(r => r.RequestID == id);
				return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}