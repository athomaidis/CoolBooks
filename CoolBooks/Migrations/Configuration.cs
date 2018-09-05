namespace CoolBooks.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using CoolBooks.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Collections.Generic;
    using System.Data;
    using System.IO;
    using Microsoft.Win32;

    internal sealed class Configuration : DbMigrationsConfiguration<CoolBooks.Models.CoolBooksContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(CoolBooks.Models.CoolBooksContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            var appContext = ApplicationDbContext.Create();
            SeedData.SeedUsers(appContext);

            var userIdTest1 = appContext.Users.Where(u => u.UserName.Equals("Test1@test.se")).FirstOrDefault().Id;
            var userIdTest2 = appContext.Users.Where(u => u.UserName.Equals("Test2@test.se")).FirstOrDefault().Id;
            var userIdTest3 = appContext.Users.Where(u => u.UserName.Equals("Test3@test.se")).FirstOrDefault().Id;
            var userIdTestAdmin1 = appContext.Users.Where(u => u.UserName.Equals("TestAdmin1@test.se")).FirstOrDefault().Id;
            var userIdTestAdmin2 = appContext.Users.Where(u => u.UserName.Equals("TestAdmin2@test.se")).FirstOrDefault().Id;

           
            context.Users.AddOrUpdate(
                 u=>u.FirstName,
                 new Users { UserId = userIdTest1,FirstName="Sigurd",LastName="Fafnesbane", Address="Valhallsvägen 1", City ="Valhall", Country ="Asgård", Email ="Test1@test.se", Birthdate =new DateTime(1901,01,01), Gender = "M", Info ="Slayer of Fafner", Phone = "123456789", Created= DateTime.Now, ZipCode="40101" },
                 new Users { UserId = userIdTest2, FirstName = "Ragnar", LastName = "Lodbrok", Address = "Valhallsvägen 2", City = "Valhall", Country = "Asgård", Email = "Test2@test.se", Birthdate = new DateTime(1901, 01, 01), Gender = "M", Info = "Slayer of dragon", Phone = "123456789", Created = DateTime.Now, ZipCode = "40102" },
                 new Users { UserId = userIdTest3, FirstName = "Olof", LastName = "Tryggvesson", Address = "Valhallsvägen 3", City = "Valhall", Country = "Asgård", Email = "Test3@test.se", Birthdate = new DateTime(1901, 01, 01), Gender = "M", Info = "King", Phone = "123456789", Created = DateTime.Now, ZipCode = "40101" },
                 new Users { UserId = userIdTestAdmin1, FirstName = "Oden", LastName = "Allfader", Address = "Valhallsvägen 4", City = "Valhall", Country = "Asgård", Email = "TestAdmin1@test.se", Birthdate = new DateTime(1901, 01, 01), Gender = "M", Info = "Herfather", Phone = "123456789", Created = DateTime.Now, ZipCode = "40101" },
                 new Users { UserId = userIdTestAdmin2, FirstName = "Tor", LastName = "Odensson", Address = "Valhallsvägen 5", City = "Valhall", Country = "Asgård", Email = "TestAdmin2@test.se", Birthdate = new DateTime(1901, 01, 01), Gender = "M", Info = "good goth Good", Phone = "123456789", Created = DateTime.Now, ZipCode = "40101" }
                );
            context.SaveChanges();

            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(appContext));
            userManager.AddToRole(userIdTest1, "BasicUser");
            userManager.AddToRole(userIdTest2, "BasicUser");
            userManager.AddToRole(userIdTest3, "BasicUser");
            userManager.AddToRole(userIdTestAdmin1, "Admin");
            userManager.AddToRole(userIdTestAdmin2, "Admin");

            // Book information from www.adlibris.com
            context.Authors.AddOrUpdate(
                a => a.FirstName,
                new Authors { FirstName = "Unknown", LastName = "Author", Created = DateTime.Now, Description="An unknown/forgotten author." },
                new Authors { FirstName = "Alexander", LastName = "Bågenholm", Created = DateTime.Now, Description = "An translator an author." },
                new Authors { FirstName = "Lars Magnar", LastName = "Enoksen", Created = DateTime.Now, Description = "An author." },
                new Authors { FirstName = "Saxo", LastName = "Grammaticus", Created = DateTime.Now, Description = "An old danish author." },
                new Authors { FirstName = "Henrik", LastName = "Hallgren", Created = DateTime.Now, Description = "An author." },
                new Authors { FirstName = "Magnus", LastName = "Lilja", Created = DateTime.Now, Description = "Another author." },
                new Authors { FirstName = "Adam", LastName = "Freeman", Created = DateTime.Now, Description = "Another author." },
                new Authors { FirstName = "Jon", LastName = "Galloway", Created = DateTime.Now, Description = "Another author." },
                new Authors { FirstName = "Arnaud", LastName = "Weil", Created = DateTime.Now, Description = "Another author." },
                new Authors { FirstName = "Lee", LastName = "Naylor", Created = DateTime.Now, Description = "Another author." },
                new Authors { FirstName = "Jonas", LastName = "Fagerberg", Created = DateTime.Now, Description = "Another author." },
                new Authors { FirstName = "Patrick", LastName = "Desjardins", Created = DateTime.Now, Description = "Another author." },
                new Authors { FirstName = "Jamie", LastName = "Munro", Created = DateTime.Now, Description = "Another author." },
                new Authors { FirstName = "Ben", LastName = "Forta", Created = DateTime.Now, Description = "Another SQL author." },
                new Authors { FirstName = "Thomas", LastName = "Nield", Created = DateTime.Now, Description = "An SQL author." },
                new Authors { FirstName = "Chris", LastName = "Fehily", Created = DateTime.Now, Description = "SQL author." },
                new Authors { FirstName = "Betty", LastName = "Smith", Created = DateTime.Now, Description = "Author." }
                );

            context.Genres.AddOrUpdate(
                g=>g.Name,
                new Genres { Name="Religious",  Description="Religion and mythology", Created=DateTime.Now },
                new Genres { Name = "Fiction", Description = "Made up stories", Created = DateTime.Now },
                new Genres { Name = "Fact", Description = "Non fiction", Created = DateTime.Now },
                new Genres { Name = "Saga", Description = "Sagas", Created = DateTime.Now },
                new Genres { Name = "Programming", Description = "Programming languages", Created = DateTime.Now },
                new Genres { Name = "Tradgedy", Description = "Tragedy is a form of drama based on human suffering that invokes an accompanying catharsis or pleasure in audiences.", Created = DateTime.Now },
                new Genres { Name = "Tragic comedy", Description = "Tragicomedy is a literary genre that blends aspects of both tragic and comic forms.", Created = DateTime.Now },
                new Genres { Name = "Fantasy", Description = "Fantasy is a genre of speculative fiction set in a fictional universe.", Created = DateTime.Now },
                new Genres { Name = "Mythology", Description = "Mythology refers variously to the collected myths of a group of people or to the study of such myths", Created = DateTime.Now },
                new Genres { Name = "Adventure", Description = "Adventure fiction is fiction that usually presents danger, or gives the reader a sense of excitement.", Created = DateTime.Now },
                new Genres { Name = "Mystery", Description = "Mystery fiction is a genre of fiction usually involving a mysterious death or a crime to be solved.", Created = DateTime.Now },
                new Genres { Name = "Drama", Description = "Drama is the specific mode of fiction represented in performance: a play performed in a theatre, or on radio or television.", Created = DateTime.Now },
                new Genres { Name = "Science fiction", Description = "Science fiction (often shortened to Sci-Fi or SF) is a genre of speculative fiction.", Created = DateTime.Now },
                new Genres { Name = "Romance", Description = "A genre fiction involving love and lust.", Created = DateTime.Now },
                new Genres { Name = "Satire", Description = "Satire is a genre of literature, and sometimes graphic and performing arts, in which vices, follies, abuses, and shortcomings are held up to ridicule, ideally with the intent of shaming individuals, corporations, government, or society itself into improvement.", Created = DateTime.Now },
                new Genres { Name = "Horror", Description = "Horror is a genre of speculative fiction which is intended to, or has the capacity to frighten, scare, disgust, or startle its readers or viewers by inducing feelings of horror and terror.", Created = DateTime.Now }
                );

            context.SaveChanges();

            var authorId = context.Authors.Where(a => a.LastName.Equals("Bågenholm")).FirstOrDefault().Id;
            var authorId2 = context.Authors.Where(a => a.LastName.Equals("Enoksen")).FirstOrDefault().Id;
            var authorId3 = context.Authors.Where(a => a.LastName.Equals("Grammaticus")).FirstOrDefault().Id;
            var authorId4 = context.Authors.Where(a => a.FirstName.Equals("Unknown")).FirstOrDefault().Id;
            var authorId5 = context.Authors.Where(a => a.FirstName.Equals("Henrik")).FirstOrDefault().Id;
            var authorId6 = context.Authors.Where(a => a.FirstName.Equals("Magnus")).FirstOrDefault().Id;
            var authorId7 = context.Authors.Where(a => a.FirstName.Equals("Adam")).FirstOrDefault().Id;
            var authorId8 = context.Authors.Where(a => a.FirstName.Equals("Jon")).FirstOrDefault().Id;
            var authorId9 = context.Authors.Where(a => a.FirstName.Equals("Arnaud")).FirstOrDefault().Id;
            var authorId10 = context.Authors.Where(a => a.FirstName.Equals("Lee")).FirstOrDefault().Id;
            var authorId11 = context.Authors.Where(a => a.FirstName.Equals("Jonas")).FirstOrDefault().Id;
            var authorId12 = context.Authors.Where(a => a.FirstName.Equals("Patrick")).FirstOrDefault().Id;
            var authorId13 = context.Authors.Where(a => a.FirstName.Equals("Jamie")).FirstOrDefault().Id;
            var authorId14 = context.Authors.Where(a => a.FirstName.Equals("Ben")).FirstOrDefault().Id;
            var authorId15 = context.Authors.Where(a => a.FirstName.Equals("Thomas")).FirstOrDefault().Id;
            var authorId16 = context.Authors.Where(a => a.FirstName.Equals("Chris")).FirstOrDefault().Id;
            var authorId17 = context.Authors.Where(a => a.FirstName.Equals("Betty")).FirstOrDefault().Id;


            var genreId = context.Genres.Where(a => a.Name.Equals("Religious")).FirstOrDefault().Id;
            var genreId2 = context.Genres.Where(a => a.Name.Equals("Fact")).FirstOrDefault().Id;
            var genreId3 = context.Genres.Where(a => a.Name.Equals("Saga")).FirstOrDefault().Id;
            var genreId4 = context.Genres.Where(a => a.Name.Equals("Programming")).FirstOrDefault().Id;
            var genreId5 = context.Genres.Where(a => a.Name.Equals("Drama")).FirstOrDefault().Id;

            context.Books.AddOrUpdate(
            b=>b.Title,
            new Books { Title= "Den Poetiska Eddan - Gudasångerna - i nyöversättning", AlternativeTitle="Den älde eddan", Created = DateTime.Now, AuthorId= authorId , GenreId= genreId, Description= "För länge sedan, när människorna skyddades av åskguden Tor och offrade åt den mäktiga gudinnan Freja, då var världens öde redan förutbestämt. Världsträdets rötter låg djupt rotade i jorden och i de nio världarna slogs och älskade jättar, människor, gudar och alver. Då, på vikingatiden, sjöng och diktade nordens folk om dessa varelsers märkliga äventyr. De dikterna sammanställdes av en okänd författare i vad som kom att kalls för Den poetiska eddan.För första gången är en av Nordens största kulturskatter översatt på ett modernt och lättillgängligt språk. Läsaren behöver inga förkunskaper i ämnet. Med en översiktlig presentation får vi en känsla för den tid och det rum som dikterna utspelar sig i. Boken kompletteras dessutom av kommentarer och en utförlig ordlista.Den poetiska eddan är nyöversatt från fornisländskan av religionshistorikern Alexander Bågenholm. Han har tidigare givit ut hednisk kalender (2012) och skrivit ett antal artiklar, samt hållit i föreläsningar kring religion och kultur.", PublishDate=new DateTime(2013,10,01), ISBN= "9789163735912", Part =1, UserId= userIdTest1, ImagePath= "/Images/den-poetiska-eddan---gudasangerna---i-nyoversattning.jpg" },
            new Books { Title = "Runor : historia, tydning, tolkning", Created = DateTime.Now, AuthorId = authorId2, GenreId = genreId2, Description = "Enoksen förklarar runornas mysterier på ett rättframt, livfullt och oakademiskt vis.”Svenska DagbladetI spåren av det ökande intresset för historia har nyfikenheten för våra äldsta skrivtecken vuxit fram. Mycket har skrivits om runor; mycket har varit spekulativt och överdrivet. 'Runor – historia, tydning, tolkning' är runboken som med engagemang och stor sakkunskap förklarar runornas utveckling under olika tidsperioder.Lars Magnar Enoksen förklarar bland annat stavningsregler, grafisk uppbyggnad och uttal. Han hämtar konkreta exempel från olika runinskrifter i Norden och bokens många illustrationer hjälper läsaren att själv tyda och tolka runinskrifter.Vidare presenteras översiktligt runforskningens historia – från 1500- och 1600-talens nyväckta intresse fram till dagens teorier om skriftens ursprung.Lars Magnar Enoksen är författare och har även gett ut böcker om mytologi och fornminnen samt noveller och historiska romaner. 'En både underhållande och lärorik bok som kastar ljus över en del av vårt kulturarv.' Västerbottens Folkblad.", PublishDate = new DateTime(2014, 02, 01), ISBN = "9789175930930", Part = 1, UserId = userIdTest1, ImagePath = "/Images/runor-historia-tydning-tolkning.jpg" },
            new Books { Title = "Gesta Danorum",AlternativeTitle= "Deeds of the Danes" , Created = DateTime.Now, AuthorId = authorId3, GenreId = genreId, Description = "Gesta Danorum - Deeds of the Danes In the early years of the thirteenth century the Danish writer Saxo Grammaticus provided his people with a History of the Danes, an account of their glorious past from the legendary kings and heroes of Denmark to king Gorm. It is one of the major sources for the heroic and mythological traditions of northern Europe, though the complex Latin style and the wide range of material brought together from different sources have limited its use. ", PublishDate = new DateTime(2016, 02, 01), ISBN = "9781329902831 ", Part = 1, UserId = userIdTest2, ImagePath = "/Images/gesta-danorum---deeds-of-the-danes.jpg" },
            new Books { Title = "SWE-THE PROSE OR YOUNGER EDDA", AlternativeTitle = "Younger edda", Created = DateTime.Now, AuthorId = authorId4, GenreId = genreId, Description = "Beskrivning saknas från förlaget. Kolla gärna upp förlagets (WENTWORTH PR) hemsida, där det kan finnas mer information.", PublishDate = new DateTime(2016, 08, 01), ISBN = "9781372655081", Part = 1, UserId = userIdTest2, ImagePath = "/Images/swe-the-prose-or-younger-edda.jpg" },
            new Books { Title = "Isländska sagor del 1", Created = DateTime.Now, AuthorId = authorId4, GenreId = genreId3, Description = "Beskrivning saknas från förlaget. Kolla gärna upp förlagets (Modernista) hemsida, där det kan finnas mer information.", PublishDate = new DateTime(2019, 05, 01), ISBN = "9789174998733", Part = 1, UserId = userIdTest2, ImagePath = "/Images/islandska-sagor-del-1.jpg" },
            new Books { Title = "Isländska sagor del 2", Created = DateTime.Now, AuthorId = authorId4, GenreId = genreId3, Description = "Beskrivning saknas från förlaget. Kolla gärna upp förlagets (Modernista) hemsida, där det kan finnas mer information.", PublishDate = new DateTime(2019, 05, 01), ISBN = "9789174998757", Part = 2, UserId = userIdTest2, ImagePath = "/Images/islandska-sagor-del-2.jpg" },
            new Books { Title = "Isländska sagor del 3", Created = DateTime.Now, AuthorId = authorId4, GenreId = genreId3, Description = "Beskrivning saknas från förlaget. Kolla gärna upp förlagets (Modernista) hemsida, där det kan finnas mer information.", PublishDate = new DateTime(2019, 05, 01), ISBN = "9789174998771", Part = 3, UserId = userIdTest2, ImagePath = "/Images/islandska-sagor-del-3.jpg" },
            new Books { Title = "Isländska sagor del 4", Created = DateTime.Now, AuthorId = authorId4, GenreId = genreId3, Description = "Beskrivning saknas från förlaget. Kolla gärna upp förlagets (Modernista) hemsida, där det kan finnas mer information.", PublishDate = new DateTime(2019, 05, 01), ISBN = "9789174998795", Part = 4, UserId = userIdTest2, ImagePath = "/Images/islandska-sagor-del-4.jpg" },
            new Books { Title = "Edda: Snorres Edda & Den poetiska Eddan", AlternativeTitle = "Snorres eddan och den poetiska", Created = DateTime.Now, AuthorId = authorId4, GenreId = genreId, Description = "DEN MEST KOMPLETTA EDDAN PÅ SVENSKA! Innehåller: Snorres Edda - Den poetiska Eddan - 5 ytterligare Eddadikter - Utförliga kommentarer Eddan är en av världslitteraturens stora texter, ständigt refererad till i litteratur och forskning.Den är vår huvudkälla till den nordiska mytologin: ogripbar, undflyende och samtidigt omätligt rik. Materialet har bevarats till eftervärlden i två former: Snorres Edda och Den poetiska Eddan.* Snorres Edda inleds med Gylfaginning, där den svenske kungen Gylfe, också kallad Ganglere,bestämmer sig för att ta reda på mer om asarna och beger sig till Asgård.Där träffar han i Valhall tre visa män, Hög, Jämnhög och Tredje, vilka egentligen är ögonvändelser av Oden.Genom dessa tre berättar nu Oden för Ganglere om gudavärlden, om skapelsen, Ginnungagap, Yggdrasil, Mimers brunn, nornorna, jättarna och så vidare.Han berättar även om de många äventyr gudarna gjorde.Om Tors äventyr i Utgård och mötet med Utgårda - Loke, och om när Tor tampas med Midgårdsormen...." , PublishDate = new DateTime(2013, 10, 01), ISBN = "9789187593499", Part = 1, UserId = userIdTest1, ImagePath = "/Images/edda-snorres-edda-den-poetiska-eddan.jpg" },
            new Books { Title = "Ord om sed : hedniska betraktelser vid dagens början", Created = DateTime.Now, AuthorId = authorId5, GenreId = genreId, Description = "Det blotas fortfarande till Oden, Freja och tomten i Sverige. Tomten får gröt till jul och Freja firas vid midsommar. Den forna seden, asatron, har överlevt in i det moderna samhället och återupptäcks av nya generationer hedningar. Sedan 1997 har Sveriges Asatrosamfund delat med sig av hedniska betraktelser i livsåskådningsprogrammet ? Vid dagens början i Sveriges radios P1. I ? Ord om Sed ? har vi samlat dessa 22 hedniska reflektioner för att ge fler möjlighet att ta del av tankarna och få insikt i den moderna hedendomen.", PublishDate = new DateTime(2019, 05, 01), ISBN = "9789197882903", Part = 1, UserId = userIdTest2, ImagePath = "/Images/ord-om-sed-hedniska-betraktelser-vid-dagens-borjan.jpg" },
            new Books { Title = "Programmering 1 C#", Created = DateTime.Now, AuthorId = authorId6, GenreId = genreId4, Description = "Programmering är ett basläromedel i programmering för gymnasiet anpassat efter Gy11. Programmering 1 C# lär ut grunderna i programmering utifrån programspråket C#. Programmering 1 C# kräver ingen tidigare erfarenhet av programmering och innehållets struktur gör det lätt att arbeta självständigt. Till alla repetitionsfrågor och övningar finns det lösningar. I texten finns det gott om exempel och ett antal projektuppgifter avslutar boken. Boken är bunden i ringpärm för praktisk användning vid datorn.Till boken finns en lärarwebb med lösningar, provförslag, extrauppgifter och annat som underlättar lärarens arbete. Programmering 1 C# i korthet: • har en tydlig struktur och genomtänkta övningar • underlättar självstudier för eleverna • har ett uppdaterat lärarmaterial på webben", PublishDate = new DateTime(2019, 05, 01), ISBN = "9789140674029", Part = 1, UserId = userIdTest2, ImagePath = "/Images/programmering-1-c.jpg" },
            new Books { Title = "Pro ASP.NET MVC 5", Created = DateTime.Now, AuthorId = authorId7, GenreId = genreId4, Description = "The ASP.NET MVC 5 Framework is the latest evolution of Microsoft's ASP.NET web platform. It provides a high-productivity programming model that promotes cleaner code architecture, test-driven development, and powerful extensibility, combined with all the benefits of ASP.NET.ASP.NET MVC 5 contains a number of advances over previous versions, including the ability to define routes using C# attributes and the ability to override filters. The user experience of building MVC applications has also been substantially improved. The new, more tightly integrated, Visual Studio 2013 IDE has been created specifically with MVC application development in mind and provides a full suite of tools to improve development times and assist in reporting, debugging and deploying your code. The popular Bootstrap JavaScript library has also now been included natively within MVC 5 providing you, the developer, with a wider range of multi-platform CSS and HTML5 options than ever before without the penalty of having to load-in third party libraries. ", PublishDate = new DateTime(2013, 12, 01), ISBN = "9781430265290", Part = 1, UserId = userIdTest2, ImagePath = "/Images/pro-aspnet-mvc-5.jpg" },
            new Books { Title = "Professional ASP.NET MVC 5", Created = DateTime.Now, AuthorId = authorId8, GenreId = genreId4, Description = "MVC 5 is the newest update to the popular Microsoft technology that enables you to build dynamic, data-driven websites. Like previous versions, this guide shows you step-by-step techniques on using MVC to best advantage, with plenty of practical tutorials to illustrate the concepts. It covers controllers, views, and models; forms and HTML helpers; data annotation and validation; membership, authorization, and security.MVC 5, the latest version of MVC, adds sophisticated features such as single page applications, mobile optimization, and adaptive rendering A team of top Microsoft MVP experts, along with visionaries in the field, provide practical advice on basic and advanced MVC topics Covers controllers, views, models, forms, data annotations, authorization and security, Ajax, routing, ASP.NET web API, dependency injection, unit testing, real - world application, and much more Professional ASP.NET MVC 5 is the comprehensive resource you need to make the best use of the updated Model - View - Controller technology.", PublishDate = new DateTime(2014, 08, 01), ISBN = "9781118794753", Part = 1, UserId = userIdTest2, ImagePath = "/Images/professional-aspnet-mvc-5.jpg" },
            new Books { Title = "Learn ASP.NET Mvc", Created = DateTime.Now, AuthorId = authorId9, GenreId = genreId4, Description = "You're a developper who knows nothing to ASP.NET MVC. Which is fine, except that you need to start coding your next application using ASP.NET MVC 5. Don't worry: I have you covered. I've been training hundreds of developers like you during years, and converted my experience into this book. I know from experience teaching what takes more time to learn in ASP.NET MVC, and will spend time only where appropriate. Read this book, and you can code your ASP.NET MVC 5 application using Visual Studio 2015 within a week. ", PublishDate = new DateTime(2015, 11, 01), ISBN = "9781326483036", Part = 1, UserId = userIdTest2, ImagePath = "/Images/learn-aspnet-mvc.jpg" },
            new Books { Title = "ASP.NET MVC with Entity Framework and CSS", Created = DateTime.Now, AuthorId = authorId10, GenreId = genreId4, Description = "Get up and running quickly with Microsoft ASP.NET MVC and Entity Framework as you build and deploy complex websites. By using a fully working example retail website you will learn solutions to real-world issues that developers frequently encounter.Whether you are a novice developer or an experienced .NET developer wishing to learn more about MVC and Entity Framework, author Lee Naylor will teach you how to develop a detailed database-driven example website using Microsoft ASP.NET and Entity Framework Code First with fully explained code examples.What You Will Learn:* Get up and running quickly with ASP.NET MVC and Entity Framework to build a complex website to display and manage several related entities* Integrate identity code into a project* Understand advanced topics, including asynchronous database access and managing data conflicts* Work with Microsoft Azure, including remote debugging and database access* Develop your CSS skills, including animations and media queries for use with tablet or mobile/cell phone devicesWho This Book Is For:Novice developers new to the subject through to more experienced ASP.NET web forms developers looking to migrate from web forms to MVC and Entity Framework. The book assumes some programming knowledge such as object-oriented programming concepts and a basic knowledge of C#.", PublishDate = new DateTime(2016, 09, 01), ISBN = "9781484221365", Part = 1, UserId = userIdTest2, ImagePath = "/Images/aspnet-mvc-with-entity-framework-and-css.jpg" },
            new Books { Title = "ASP.Net MVC 5 - Building a Website with Visual Studio 2015 and C Sharp: The Tactical Guidebook", Created = DateTime.Now, AuthorId = authorId11, GenreId = genreId4, Description = "This book is primarily aimed at developers who have some prior experience with MVC 5 and are proficient in C#, since the language won't be explained in any detail. Even if you already have created a couple of MVC projects, you might find the content in this book useful as a refresher. You might have worked in previous versions of Visual Studio and MVC and want a fast no-fluff way to get up to speed with MVC 5. Prerequisites: -C# (you need to be proficient) -MVC 5 (you need some prior experience) -HTML5/CSS3 (you need basic knowledge) -JavaScript/JQuery (you need basic knowledge) In this book you will learn how to build an MVC 5 website using C#, Entity Framework, HTML5, CSS, JavaScript and Ajax. -Create a 'real world' code-first database using Entity Framework. -Add and modify models, views and controllers to perform CRUD operations against the database. -Use client-side and server-side validation. -Secure the controllers, actions and view content with authorization and roles. -Implement security with ASP.NET Identity (authentication, authorization and roles). -Use JavaScript, JQuery and Ajax to build a great end-user interface with asynchronous server calls. -Style the user interfaces using CSS3 and Bootstrap. -Create a responsive website. -Implement product subscription scenarios with registration codes. -Create an alternate way to login to the application. -Implement password reset functionality. -Register users with the site. -Manage users and their subscriptions. Technologies, frameworks and languages: -C# (you need to be proficient) -HTML5/CSS3 (you need basic knowledge) -JavaScript/JQuery (you need basic knowledge) -MVC 5 (you will create/modify models, views and controllers) -Razor syntax (to include server-side code in views) -Bootstrap (used for styling and to create a responsive design) -Ajax (used for asynchronous server calls) ... ", PublishDate = new DateTime(2016, 07, 01), ISBN = "9781535167864", Part = 1, UserId = userIdTest2, ImagePath = "/Images/aspnet-mvc-5---building-a-website-with-visual-studio-2015-and-c-sharp-the-tactical-guidebook.jpg" },
            new Books { Title = ".Net Knowledge Book: Web Development with ASP.Net MVC and Entity Framework: .Net Knowledge Book: Web Development with ASP.Net MVC and Entit", Created = DateTime.Now, AuthorId = authorId12, GenreId = genreId4, Description = "Beskrivning saknas från förlaget. Kolla gärna upp förlagets (Depot Legal - Bibliotheque Et Archives Nation) hemsida, där det kan finnas mer information.", PublishDate = new DateTime(2014, 06, 01), ISBN = "9782981311016", Part = 1, UserId = userIdTest2, ImagePath = "/Images/net-knowledge-book-web-development-with-aspnet-mvc-and-entity-framework-net-knowledge-book-web-development-with-aspnet-mvc-and-entit.jpg" },
            new Books { Title = "ASP.NET MVC 5 with Bootstrap and Knockout.js", Created = DateTime.Now, AuthorId = authorId13, GenreId = genreId4, Description = "Bring dynamic server-side web content and responsive web design together to build websites that work and display well on any resolution, desktop or mobile. With this practical book, youll learn how by combining the ASP.NET MVC server-side language, the Bootstrap front-end framework, and Knockout.jsthe JavaScript implementation of the Model-View-ViewModel pattern.Author Jamie Munro introduces these and other related technologies by having you work with sophisticated web forms. At the end of the book, experienced and aspiring web developers alike will learn how to build a complete shopping cart that demonstrates how these technologies interact with each other in a sleek, dynamic, and responsive web application.Build well-organized, easy-to-maintain web applications by letting ASP.NET MVC 5, Bootstrap, and Knockout.js do the heavy liftingUse ASP.NET MVC 5 to build server-side web applications, interact with a database, and dynamically render HTMLCreate responsive views with Bootstrap that render on a variety of modern devices; you may never code with CSS againAdd Knockout.js to enhance responsive web design with snappy client-side interactions driven by your server-side web application", PublishDate = new DateTime(2015, 05, 01), ISBN = "9781491914403", Part = 1, UserId = userIdTest2, ImagePath = "/Images/aspnet-mvc-5-with-bootstrap-and-knockoutjs.jpg" },
            new Books { Title = "Kom igång med SQL", Created = DateTime.Now, AuthorId = authorId14, GenreId = genreId4, Description = "Den enkla vägen till snabbare resultat. En kompakt och praktiskt upplagd bok som varvar teori med praktiska exempel. Genom att steg för steg ta dig igenom kapitlen, lär du dig snabbt och lätt allt du behöver veta för att utnyttja de nya funktionerna i SQL. Korta kapitel som visar hur du använder olika SQL - satser skapar komplicerade SQL - satser med flera instruktioner och operatorer hämtar information från databaser sorterar och formaterar den data du hämtar använder olika tekniker för att filtrera data använder mängdfunktioner för att summera data kopplar samman två eller flera relaterade tabeller infogar, uppdaterar och tar bort data skapar och ändrar tabeller arbetar med vyer Ur innehållet: Vad är SQL?.Hämta data.Sortera data.Filtrera data.Avancerad datafiltrering.Filtrera med jokertecken.Skapa beräknade fält.Använda funktioner.Summera funktioner.Summera data.Gruppera data.Arbeta med underfrågor.Koppla samman tabeller.Skapa avancerade kopplingar.Kombinera frågor.Infoga data.Uppdatera och ta bort data.Skapa och bearbeta tabeller.Använda vyer.Arbeta med lagrade procedurer.Använda transaktionshantering.Använda markörer.Avancerade funktioner i SQL.Datatyper.Reserverade ord", PublishDate = new DateTime(2006, 06, 01), ISBN = "9789163609046", Part = 1, UserId = userIdTest2, ImagePath = "/Images/kom-igang-med-sql.jpg" },
            new Books { Title = "Getting Started with SQL: A Hands - On Approach for Beginners", Created = DateTime.Now, AuthorId = authorId15, GenreId = genreId4, Description = "Businesses are gathering data today at exponential rates and yet few people know how to access it meaningfully. If you re a business or IT professional, this short hands-on guide teaches you how to pull and transform data with SQL in significant ways. You will quickly master the fundamentals of SQL and learn how to create your own databases. Author Thomas Nield provides exercises throughout the book to help you practice your newfound SQL skills at home, without having to use a database server environment.Not only will you learn how to use key SQL statements to find and manipulate your data, but you ll also discover how to efficiently design and manage databases to meet your needs.", PublishDate = new DateTime(2016, 02, 01), ISBN = "9781491938614", Part = 1, UserId = userIdTest2, ImagePath = "/Images/getting-started-with-sql-a-hands-on-approach-for-beginners.jpg" },
            new Books { Title = "SQL (Database Programming)", Created = DateTime.Now, AuthorId = authorId16, GenreId = genreId4, Description = "Perfect for end users, analysts, data scientists, and app developers, this best-selling guide will get you up and running with SQL, the language of databases. You'll find general concepts, practical answers, and clear explanations of what the various SQL statements can do. Hundreds of examples of varied difficulty encourage you to experiment and explore. SQL code listings help you see the elements and structure of the language. You can download the sample database to follow along with the author's examples.", PublishDate = new DateTime(2014, 07, 01), ISBN = "9781937842314", Part = 1, UserId = userIdTest2, ImagePath = "/Images/sql-database-programming.jpg" },
            new Books { Title = "A Tree Grows in Brooklyn", Created = DateTime.Now, AuthorId = authorId17, GenreId = genreId5, Description = "A new edition of the classic novel, featuring a new foreword by best-selling author Anna Quindlen, follows young Francie Nolan, who is armed with her idealism and determination, as she struggles to escape from the poverty of life in a Brooklyn tenement during the early 1900s. Reader's Guide available. Reprint. 50,000 first printing. ", PublishDate = new DateTime(2005, 02, 01), ISBN = "9780060736262", Part = 1, UserId = userIdTest2, ImagePath = "/Images/a-tree-grows-in-brooklyn.jpg" }
                );


            context.SaveChanges();

            var bookId1 = context.Books.Where(a => a.Title.Equals("Den Poetiska Eddan - Gudasångerna - i nyöversättning")).FirstOrDefault().Id;
            var bookId2 = context.Books.Where(a => a.Title.Equals("Runor : historia, tydning, tolkning")).FirstOrDefault().Id;
            var bookId3 = context.Books.Where(a => a.Title.Equals("Gesta Danorum")).FirstOrDefault().Id;
            var bookId4 = context.Books.Where(a => a.Title.Equals("SWE-THE PROSE OR YOUNGER EDDA")).FirstOrDefault().Id;
            var bookId5 = context.Books.Where(a => a.Title.Equals("Isländska sagor del 1")).FirstOrDefault().Id;
            var bookId6 = context.Books.Where(a => a.Title.Equals("Isländska sagor del 2")).FirstOrDefault().Id;
            var bookId7 = context.Books.Where(a => a.Title.Equals("Isländska sagor del 3")).FirstOrDefault().Id;
            var bookId8 = context.Books.Where(a => a.Title.Equals("Isländska sagor del 4")).FirstOrDefault().Id;
            var bookId9 = context.Books.Where(a => a.Title.Equals("Edda: Snorres Edda & Den poetiska Eddan")).FirstOrDefault().Id;
            var bookId10 = context.Books.Where(a => a.Title.Equals("Ord om sed : hedniska betraktelser vid dagens början")).FirstOrDefault().Id;
            var bookId11 = context.Books.Where(a => a.Title.Equals("Programmering 1 C#")).FirstOrDefault().Id;
            var bookId12 = context.Books.Where(a => a.Title.Equals("Pro ASP.NET MVC 5")).FirstOrDefault().Id;
            var bookId13 = context.Books.Where(a => a.Title.Equals("Professional ASP.NET MVC 5")).FirstOrDefault().Id;
            var bookId14 = context.Books.Where(a => a.Title.Equals("Learn ASP.NET Mvc")).FirstOrDefault().Id;
            var bookId15 = context.Books.Where(a => a.Title.Equals("ASP.NET MVC with Entity Framework and CSS")).FirstOrDefault().Id;
            var bookId16 = context.Books.Where(a => a.Title.Equals("ASP.Net MVC 5 - Building a Website with Visual Studio 2015 and C Sharp: The Tactical Guidebook")).FirstOrDefault().Id;
            var bookId17 = context.Books.Where(a => a.Title.Equals(".Net Knowledge Book: Web Development with ASP.Net MVC and Entity Framework: .Net Knowledge Book: Web Development with ASP.Net MVC and Entit")).FirstOrDefault().Id;
            var bookId18 = context.Books.Where(a => a.Title.Equals("ASP.NET MVC 5 with Bootstrap and Knockout.js")).FirstOrDefault().Id;
            var bookId19 = context.Books.Where(a => a.Title.Equals("Kom igång med SQL")).FirstOrDefault().Id;
            var bookId20 = context.Books.Where(a => a.Title.Equals("Getting Started with SQL: A Hands - On Approach for Beginners")).FirstOrDefault().Id;
            var bookId21 = context.Books.Where(a => a.Title.Equals("SQL (Database Programming)")).FirstOrDefault().Id;
            var bookId22 = context.Books.Where(a => a.Title.Equals("A Tree Grows in Brooklyn")).FirstOrDefault().Id;


            context.Reviews.AddOrUpdate(
            b => b.Title,
            new Reviews { UserId = userIdTest1,Title="I liked this book",Text="Good book. The best i have read.", Created =DateTime.Now, Rating =5, BookId = bookId1 },
            new Reviews { UserId = userIdTest2, Title = "I also liked this book", Text = "Best book", Created = DateTime.Now, Rating = 4, BookId = bookId1 },
            new Reviews { UserId = userIdTest2, Title = "I read this book", Text = "The best book", Created = DateTime.Now, Rating = 4, BookId = bookId2 },
            new Reviews { UserId = userIdTest3, Title = "I have read this book", Text = "Great book", Created = DateTime.Now, Rating = 4, BookId = bookId2 },
            new Reviews { UserId = userIdTest3, Title = "I hated this book", Text = "Worst book. Didn't learn anything.", Created = DateTime.Now, Rating = 1, BookId = bookId15 },
            new Reviews { UserId = userIdTestAdmin1, Title = "I also didn´t like this book", Text = "A bad book. It was too easy.", Created = DateTime.Now, Rating = 2, BookId = bookId15 },
            new Reviews { UserId = userIdTestAdmin2, Title = "I didn´t like this book", Text = "It was too easy.", Created = DateTime.Now, Rating = 2, BookId = bookId15 }
            );

            UploadBookImage(bookId1, "/Images/den-poetiska-eddan---gudasangerna---i-nyoversattning.jpg");
            UploadBookImage(bookId2, "/Images/runor-historia-tydning-tolkning.jpg");
            UploadBookImage(bookId3, "/Images/gesta-danorum---deeds-of-the-danes.jpg");
            UploadBookImage(bookId4, "/Images/swe-the-prose-or-younger-edda.jpg");
            UploadBookImage(bookId5, "/Images/islandska-sagor-del-1.jpg");
            UploadBookImage(bookId6, "/Images/islandska-sagor-del-2.jpg");
            UploadBookImage(bookId7, "/Images/islandska-sagor-del-3.jpg");
            UploadBookImage(bookId8, "/Images/islandska-sagor-del-4.jpg");
            UploadBookImage(bookId9, "/Images/edda-snorres-edda-den-poetiska-eddan.jpg");
            UploadBookImage(bookId10, "/Images/ord-om-sed-hedniska-betraktelser-vid-dagens-borjan.jpg");
            UploadBookImage(bookId11, "/Images/programmering-1-c.jpg");
            UploadBookImage(bookId12, "/Images/pro-aspnet-mvc-5.jpg");
            UploadBookImage(bookId13, "/Images/professional-aspnet-mvc-5.jpg");
            UploadBookImage(bookId14, "/Images/learn-aspnet-mvc.jpg");
            UploadBookImage(bookId15, "/Images/aspnet-mvc-with-entity-framework-and-css.jpg");
            UploadBookImage(bookId16, "/Images/aspnet-mvc-5---building-a-website-with-visual-studio-2015-and-c-sharp-the-tactical-guidebook.jpg");
            UploadBookImage(bookId17, "/Images/net-knowledge-book-web-development-with-aspnet-mvc-and-entity-framework-net-knowledge-book-web-development-with-aspnet-mvc-and-entit.jpg");
            UploadBookImage(bookId18, "/Images/aspnet-mvc-5-with-bootstrap-and-knockoutjs.jpg");
            UploadBookImage(bookId19, "/Images/kom-igang-med-sql.jpg");
            UploadBookImage(bookId20, "/Images/getting-started-with-sql-a-hands-on-approach-for-beginners.jpg");
            UploadBookImage(bookId21, "/Images/sql-database-programming.jpg");
            UploadBookImage(bookId22, "/Images/a-tree-grows-in-brooklyn.jpg");

            UploadUserImage(userIdTest1, "/Images/SigurdFafnesbane.jpg");
            UploadUserImage(userIdTest2, "/Images/RagnarLodbrok.jpg");
            UploadUserImage(userIdTest3, "/Images/OlovTrygvesson.jpg");
            UploadUserImage(userIdTestAdmin1, "/Images/Oden.jpg");
            UploadUserImage(userIdTestAdmin2, "/Images/Tor.jpg");
        }

        private void UploadBookImage(int id,string fileName)
        {
            fileName = AppContext.BaseDirectory.Remove(AppContext.BaseDirectory.Length-4,3) + fileName;
            using (FileStream fs = new FileStream(fileName, FileMode.Open,FileAccess.Read))
            using (CoolBooksContext context = new CoolBooksContext())
            {
                Books book = context.Books.Find(id);
                book.Image = new byte[fs.Length];
                fs.Read(book.Image,0,(int)fs.Length);

                FileInfo fi = new FileInfo(fileName);
                book.MimeType = GetMimeType(fi);
                context.SaveChanges();
            }
        }

        private void UploadUserImage(string id, string fileName)
        {
            fileName = AppContext.BaseDirectory.Remove(AppContext.BaseDirectory.Length - 4, 3) + fileName;
            using (FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            using (CoolBooksContext context = new CoolBooksContext())
            {
                Users user = context.Users.Find(id);
                user.Image = new byte[fs.Length];
                fs.Read(user.Image, 0, (int)fs.Length);

                FileInfo fi = new FileInfo(fileName);
                user.MimeType = GetMimeType(fi);
                context.SaveChanges();
            }
        }
        // Code from 
        // https://stackoverflow.com/questions/58510/using-net-how-can-you-find-the-mime-type-of-a-file-based-on-the-file-signature
        string GetMimeType(FileInfo fileInfo)
        {
            string mimeType = "application/unknown";

            RegistryKey regKey = Registry.ClassesRoot.OpenSubKey(
                fileInfo.Extension.ToLower()
                );

            if (regKey != null)
            {
                object contentType = regKey.GetValue("Content Type");

                if (contentType != null)
                    mimeType = contentType.ToString();
            }

            return mimeType;
        }
    }
}
    