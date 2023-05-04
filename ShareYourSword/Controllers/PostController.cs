using Microsoft.AspNetCore.Mvc;
using ShareYourSword.Assets.Repositories;
using ShareYourSword.Assets;
using ShareYourSword.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;

namespace ShareYourSword.Controllers
{
    public class PostController : Controller
    {
        private readonly PostRepository _postRepository;

        public PostController(DatabaseContext context)
        {
            _postRepository = new PostRepository(context);
        }

        [AllowAnonymous]
        public IActionResult Index(string name)
        {
            var posts = string.IsNullOrWhiteSpace(name) ? _postRepository.GetAll() : _postRepository.Filter(name);

            return View(posts);
        }

        [AllowAnonymous]
        public IActionResult Details(int id)
        {
            var post = _postRepository.Get(id);

            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Post post)
        {
            if (ModelState.IsValid)
            {
                post.Likes = 0;
                _postRepository.Add(post);

                return RedirectToAction(nameof(Index));
            }
            return View(post);
        }

        public IActionResult Edit(int id)
        {
            var post = _postRepository.Get(id);

            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Post post)
        {
            if (id != post.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _postRepository.Edit(post);

                return RedirectToAction(nameof(Index));
            }

            return View(post);
        }

        public IActionResult Delete(int id)
        {
            var post = _postRepository.Get(id);

            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _postRepository.Delete(id);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Like(int id)
        {
            var post = _postRepository.Get(id);
            _postRepository.Like(post.Id);
            return RedirectToAction(nameof(Index));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
