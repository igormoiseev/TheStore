using AutoMapper;
using TheStore.Web.Domain;
using TheStore.Web.Infrastructure.Tasks;
using TheStore.Web.Models.Brand;
using TheStore.Web.Models.Category;
using TheStore.Web.Models.Characteristic;
using TheStore.Web.Models.Color;
using TheStore.Web.Models.Option;
using TheStore.Web.Models.Product;

namespace TheStore.Web
{
    public class AutoMapperConfig : IRunAtInit
    {
        public void Execute()
        {
            Mapper.CreateMap<Category, CategoryWidgetViewModel>();
            Mapper.CreateMap<Category, EditCategoryForm>();
            Mapper.CreateMap<Category, CategoryViewModel>();
            Mapper.CreateMap<Characteristic, EditCharacteristicForm>();
            Mapper.CreateMap<Option, EditOptionForm>();
            Mapper.CreateMap<Brand, EditBrandForm>();
            Mapper.CreateMap<Product, EditProductForm>();
            Mapper.CreateMap<Color, EditColorForm>();
        }
    }
}