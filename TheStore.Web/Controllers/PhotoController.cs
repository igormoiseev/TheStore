using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Web.Mvc;
using Microsoft.Web.Mvc;
using TheStore.Web.Data;
using TheStore.Web.Domain;
using TheStore.Web.Filters;
using TheStore.Web.Infrastructure;
using TheStore.Web.Infrastructure.Alerts;
using TheStore.Web.Models.Photo;

namespace TheStore.Web.Controllers
{
    public class PhotoController : TheStoreController
    {
        private readonly ApplicationDbContext _context;

        public PhotoController(ApplicationDbContext context)
        {
            _context = context;
        }

        public ActionResult New(int productId)
        {
            var product = _context.Products.SingleOrDefault(x => x.ProductId == productId);
            if (product == null)
            {
                return
                    this.RedirectToAction<ProductController>(x => x.Manage())
                        .WithError(string.Format("Товар c ID ({0}) не найден. Возможно он был удален.", productId));
            }

            var model = new NewPhotoForm
            {
                ProductId = productId,
                ProductName = product.Name,
                Photos = (List<Photo>) (product.Photos ?? new List<Photo>())
            };
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken, Log("Add new photo")]
        public ActionResult New(NewPhotoForm form)
        {
            if (ModelState.IsValid)
            {
                var product = _context.Products.SingleOrDefault(x => x.ProductId == form.ProductId);
                if (product == null)
                {
                    return
                        this.RedirectToAction<ProductController>(x => x.Manage())
                            .WithError(string.Format("Товар c ID ({0}) не найден. Возможно он был удален.", form.ProductId));
                }

                if (form.UploadedImage == null)
                {
                    return
                        this.RedirectToAction<ProductController>(x => x.Manage())
                            .WithError(string.Format("Не выбрана фотография.", form.ProductId));
                }

                var path = Path.Combine(Server.MapPath("~/Upload/Images"), Path.GetFileName(form.UploadedImage.FileName) ?? string.Empty);
                form.UploadedImage.SaveAs(path);

                var photo = new Photo
                {
                    ProductId = form.ProductId,
                    Name = form.Name,
                    Alt = form.Alt,
                    Description = form.Description,
                    ColorId = form.ColorId,
                    Src = string.Format("/Upload/Images/{0}", form.UploadedImage.FileName)
                };

                _context.Photos.Add(photo);
                _context.SaveChanges();

                return this.RedirectToAction(x => x.New(product.ProductId))
                .WithSuccess("Фотография загружена");
            }
            return this.RedirectToAction<ProductController>(x => x.Manage())
                .WithError("Ошибка при загрузке фотографии.");
        }

        public ActionResult Edit(int id)
        {
            var photo = _context.Photos.SingleOrDefault(x => x.PhotoId == id);
            if (photo == null)
            {
                return
                    this.RedirectToAction<ProductController>(x => x.Manage())
                        .WithError(string.Format("Фотография c ID ({0}) не найдена. Возможно она была удалена.", id));
            }

            var model = new EditPhotoForm
            {
                PhotoId = photo.PhotoId,
                Src = photo.Src,
                Alt = photo.Alt,
                Name = photo.Name,
                Description = photo.Description,
                ProductName = photo.Product.Name
            };

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken, Log("Update photo")]
        public ActionResult Edit(EditPhotoForm form)
        {
            if (!ModelState.IsValid)
            {
                return View(form);
            }

            var photo = _context.Photos.SingleOrDefault(x => x.PhotoId == form.PhotoId);
            if (photo == null)
            {
                return
                    this.RedirectToAction<ProductController>(x => x.Manage())
                        .WithError(string.Format("Фотография c ID ({0}) не найдена. Возможно она была удалена.", form.PhotoId));
            }

            photo.Name = form.Name;
            photo.Alt = form.Alt;
            photo.Description = form.Description;

            if (form.UploadedImage != null)
            {
                var path = Path.Combine(Server.MapPath("~/Upload/Images"), Path.GetFileName(form.UploadedImage.FileName) ?? string.Empty);
                form.UploadedImage.SaveAs(path);

                if (_context.Photos.Count(x => x.Src == photo.Src) <= 1)
                {
                    var pathOld = Server.MapPath(photo.Src);

                    if (System.IO.File.Exists(pathOld))
                    {
                        System.IO.File.Delete(pathOld);
                    }
                }

                photo.Src = string.Format("/Upload/Images/{0}", form.UploadedImage.FileName);
            }

            _context.SaveChanges();

            return this.RedirectToAction<ProductController>(x => x.Manage()).WithSuccess("Фотография успешно обновлена.");
        }

        [Log("Delete photo {id}")]
        public ActionResult Delete(int id)
        {
            var photo = _context.Photos.Find(id);
            if (photo == null)
            {
                return
                    this.RedirectToAction<ProductController>(x => x.Manage())
                        .WithError(string.Format("Фотография c ID ({0}) не найдена. Возможно она была удалена.", id));
            }

            if (_context.Photos.Count(x => x.Src == photo.Src) <= 1)
            {
                var path = Server.MapPath(photo.Src);

                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
            }

            var product = _context.Products.SingleOrDefault(x => x.ProductId == photo.ProductId);
            if (product != null)
            {
                product.Photos.Remove(photo);
            }

            _context.Photos.Remove(photo);
            _context.SaveChanges();

            return this.RedirectToAction(x => x.New(photo.ProductId)).WithSuccess(string.Format("Фотография \"{0}\" удалена.", id));
        }
    }
}