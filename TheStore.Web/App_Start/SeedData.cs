using System.Linq;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using TheStore.Web.Data;
using TheStore.Web.Domain;
using TheStore.Web.Infrastructure.Tasks;

namespace TheStore.Web
{
    public class SeedData : IRunAtStartup
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public SeedData(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _userManager.UserValidator = new UserValidator<ApplicationUser>(_userManager) { AllowOnlyAlphanumericUserNames = false };
            _roleManager = roleManager;
        }

        public void Execute()
        {
            #region Users & Roles
            if (!_context.Roles.Any())
            {
                var administratorRole = new IdentityRole("Administrator");
                _roleManager.Create(administratorRole);

                var managerRole = new IdentityRole("Manager");
                _roleManager.Create(managerRole);

                _context.SaveChanges();
            }

            if (!_context.Users.Any())
            {
                var tyanArthur = new ApplicationUser() { UserName = "tyan.arthur@chuvi.com.ua" };
                _userManager.Create(tyanArthur, "ArtTya9393");

                _userManager.AddToRole(tyanArthur.Id, "Administrator");

                var bodulNatalia = new ApplicationUser() { UserName = "bodul.natalia@chuvi.com.ua" };
                _userManager.Create(bodulNatalia, "ArtTya9393");

                _context.SaveChanges();
            }
            #endregion

            #region Categories
            if (!_context.Categories.Any())
            {

                /* TOYS
                 * --------------------------------------------------------------------------------------------------------- */
                var toys = new Category()
                {
                    Name = "Игрушки",
                    Description = "Игрушки",
                    CategoryUrl = "toys",
                    SequenceNumber = 1
                };

                var stuffedAnimals = new Category()
                {
                    Name = "Мягкие игрушки",
                    Description = "Мягкие игрушки",
                    CategoryUrl = "stuffed-animals",
                    SequenceNumber = 1
                };

                var constructors = new Category()
                {
                    Name = "Конструкторы",
                    Description = "Конструкторы",
                    CategoryUrl = "constructors",
                    SequenceNumber = 2
                };

                var dolls = new Category()
                {
                    Name = "Куклы",
                    Description = "Куклы",
                    CategoryUrl = "dolls",
                    SequenceNumber = 2
                };

                toys.Categories.Add(stuffedAnimals);
                toys.Categories.Add(constructors);
                toys.Categories.Add(dolls);

                /* PRAMS
                 * --------------------------------------------------------------------------------------------------------- */

                var prams = new Category
                {
                    Name = "Детские коляски",
                    Description = "Коляски",
                    CategoryUrl = "prams",
                    SequenceNumber = 2
                };

                var cradles = new Category
                {
                    Name = "Коляски с люлькой",
                    Description = "Коляски с люлькой",
                    CategoryUrl = "prams-cradles",
                    SequenceNumber = 1
                };

                var prams2in1 = new Category
                {
                    Name = "Коляски 2 в 1",
                    Description = "Коляски 2 в 1",
                    CategoryUrl = "prams-2-in-1",
                    SequenceNumber = 1
                };

                var prams3in1 = new Category
                {
                    Name = "Коляски 3 в 1",
                    Description = "Коляски 3 в 1",
                    CategoryUrl = "prams-3-in-1",
                    SequenceNumber = 2
                };

                var transformers = new Category
                {
                    Name = "Коляски трансформеры",
                    Description = "Коляски трансформеры",
                    CategoryUrl = "prams-transformers",
                    SequenceNumber = 3
                };

                var strollers = new Category
                {
                    Name = "Прогулочные коляски",
                    Description = "Прогулочные коляски",
                    CategoryUrl = "prams-strollers",
                    SequenceNumber = 4
                };

                var strollerCanes = new Category
                {
                    Name = "Коляски трости",
                    Description = "Коляски трости",
                    CategoryUrl = "prams-stroller-canes",
                    SequenceNumber = 5
                };

                var pramsForTwins = new Category
                {
                    Name = "Коляски для двойни",
                    Description = "Коляски для двойни",
                    CategoryUrl = "prams-for-twins",
                    SequenceNumber = 6
                };

                prams.Categories.Add(cradles);
                prams.Categories.Add(prams2in1);
                prams.Categories.Add(prams3in1);
                prams.Categories.Add(transformers);
                prams.Categories.Add(strollers);
                prams.Categories.Add(strollerCanes);
                prams.Categories.Add(pramsForTwins);

                /* ACCESSORIES
                 * --------------------------------------------------------------------------------------------------------- */

                var pramsAscessories = new Category
                {
                    Name = "Аксессуары для колясок",
                    Description = "Аксессуары для колясок",
                    CategoryUrl = "prams-accessories",
                    SequenceNumber = 3
                };

                var accessoriesCradles = new Category
                {
                    Name = "Люльки",
                    Description = "Люльки",
                    CategoryUrl = "prams-accessories-сradles",
                    SequenceNumber = 1
                };

                var accessoriesChassis = new Category
                {
                    Name = "Шасси",
                    Description = "Шасси",
                    CategoryUrl = "prams-accessories-chassis",
                    SequenceNumber = 2
                };

                var accessoriesFrames = new Category
                {
                    Name = "Рамы",
                    Description = "Рамы",
                    CategoryUrl = "prams-accessories-frames",
                    SequenceNumber = 3
                };

                var accessoriesRaincoats = new Category
                {
                    Name = "Дождевики",
                    Description = "Дождевики",
                    CategoryUrl = "prams-accessories-raincoats",
                    SequenceNumber = 4
                };

                var accessoriesBags = new Category
                {
                    Name = "Сумки",
                    Description = "Сумки",
                    CategoryUrl = "prams-accessories-bags",
                    SequenceNumber = 5
                };

                var accessoriesSleepingBag = new Category
                {
                    Name = "Спальные мешки",
                    Description = "Спальные мешки",
                    CategoryUrl = "prams-accessories-sleeping-bags",
                    SequenceNumber = 6
                };

                pramsAscessories.Categories.Add(accessoriesCradles);
                pramsAscessories.Categories.Add(accessoriesChassis);
                pramsAscessories.Categories.Add(accessoriesFrames);
                pramsAscessories.Categories.Add(accessoriesRaincoats);
                pramsAscessories.Categories.Add(accessoriesBags);
                pramsAscessories.Categories.Add(accessoriesSleepingBag);

                /* CHILDREN's TRANSPORT
                 * --------------------------------------------------------------------------------------------------------- */

                var transport = new Category
                {
                    Name = "Детский транспорт",
                    Description = "Детский транспорт",
                    CategoryUrl = "transport",
                    SequenceNumber = 4
                };

                var twoWheelsBicycle = new Category
                {
                    Name = "Двухколесные велосипеды",
                    Description = "Двухколесные велосипеды",
                    CategoryUrl = "transport-two-wheels-bicycles",
                    SequenceNumber = 1
                };

                var runcycles = new Category
                {
                    Name = "Беговелы",
                    Description = "Беговелы",
                    CategoryUrl = "transport-runcycles",
                    SequenceNumber = 2
                };

                var threeWheelsBicycle = new Category
                {
                    Name = "Трехколесные велосипеды",
                    Description = "Трехколесные велосипеды",
                    CategoryUrl = "transport-three-wheels-bicycles",
                    SequenceNumber = 3
                };

                var scooters = new Category
                {
                    Name = "Самокаты",
                    Description = "Самокаты",
                    CategoryUrl = "transport-scooters",
                    SequenceNumber = 4
                };

                transport.Categories.Add(twoWheelsBicycle);
                transport.Categories.Add(runcycles);
                transport.Categories.Add(threeWheelsBicycle);
                transport.Categories.Add(scooters);

                _context.Categories.Add(toys);
                _context.Categories.Add(prams);
                _context.Categories.Add(pramsAscessories);
                _context.Categories.Add(transport);

                _context.SaveChanges();
            }
            #endregion

            #region Brands

            if (!_context.Brands.Any())
            {
                var capella = new Brand
                {
                    Name = "Capella",
                    BrandUrl = "capella",
                    Description = "Производитель колясок Capella"
                };

                var pegPerego = new Brand
                {
                    Name = "Peg Perego",
                    BrandUrl = "peg-perego",
                    Description = "Производитель колясок Peg Perego"
                };

                var mattel = new Brand
                {
                    Name = "Mattel",
                    BrandUrl = "mattel",
                    Description = "Производитель кукол Mattel"
                };

                var simba = new Brand
                {
                    Name = "Simba",
                    BrandUrl = "simba",
                    Description = "Производитель кукол Simba"
                };

                var megabloks = new Brand
                {
                    Name = "MEGABLOKS",
                    BrandUrl = "megabloks",
                    Description = "Производитель конструкторов MEGABLOKS"
                };

                _context.Brands.Add(capella);
                _context.Brands.Add(pegPerego);
                _context.Brands.Add(mattel);
                _context.Brands.Add(simba);
                _context.Brands.Add(megabloks);

                _context.SaveChanges();
            }

            #endregion

            #region Characteristics and Options

            if (!_context.Characteristics.Any())
            {
                /* MADE IN
                 * ----------------------------------------------------------------------------------------------------------------- */
                var madeIn = new Characteristic
                {
                    Name = "Страна производитель",
                    IsFilterable = false,
                    Url = "made-in",
                    Description = "Страна производитель",
                    SequenceNumber = 1
                };

                var southCorea = new Option { Name = "Южная Корея", Description = "Южная Корея", Url = "south-corea" };

                madeIn.Options.Add(southCorea);

                _context.Characteristics.Add(madeIn);

                /* ADJUSTABLE HANDLE
                 * ----------------------------------------------------------------------------------------------------------------- */
                var adjustableParentHandle = new Characteristic
                {
                    Name = "Регулируемая родительская ручка",
                    IsFilterable = true,
                    Url = "adjustable-parent-handle",
                    Description = "Регулируемая родительская ручка",
                    SequenceNumber = 2
                };

                var adjustableParentHandleYes = new Option { Name = "Есть", Description = "Есть", Url = "yes" };
                var adjustableParentHandleNo = new Option { Name = "Нет", Description = "Нет", Url = "no" };

                adjustableParentHandle.Options.Add(adjustableParentHandleYes);
                adjustableParentHandle.Options.Add(adjustableParentHandleNo);

                _context.Characteristics.Add(adjustableParentHandle);

                /* REVERSIBLE PARENT HANDLE
                 * ----------------------------------------------------------------------------------------------------------------- */
                var reversibleParentHandle = new Characteristic
                {
                    Name = "Перекидная родительская ручка",
                    IsFilterable = true,
                    Url = "reversible-parent-handle",
                    Description = "Перекидная родительская ручка",
                    SequenceNumber = 3
                };

                var reversibleParentHandleYes = new Option { Name = "Есть", Description = "Есть", Url = "yes" };
                var reversibleParentHandleNo = new Option { Name = "Нет", Description = "Нет", Url = "no" };

                reversibleParentHandle.Options.Add(reversibleParentHandleYes);
                reversibleParentHandle.Options.Add(reversibleParentHandleNo);

                _context.Characteristics.Add(reversibleParentHandle);


                _context.SaveChanges();
            }

            #endregion
        }
    }
}