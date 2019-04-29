using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABF.Data.ABFDbModels
{
   
    public class DBInitialiser :DropCreateDatabaseIfModelChanges<ABFDbContext>
    {
        protected override void Seed(ABFDbContext context)
        {
            // Location seed
            IList<Location> initialisedLocations = new List<Location>
            {
                new Location()
                {
                    Id = 1,
                    Name = "St Mary's Church",
                    Address1 = "Churchfield Road",
                    Address2 = "Appledore",
                    PostCode = "EX39 1RL",
                    LatLong = "51.054710, -4.192693",
                    DisabledAccess = true,
                    Info = "Seating is on wooden pews. Feel free to bring a cushion.",
                    Contact = "Adam Adams 01237 123456"
                },

                new Location()
                {
                    Id = 2,
                    Name = "St Mary's Hall",
                    Address1 = "Churchfield Road",
                    Address2 = "Appledore",
                    PostCode = "EX39 1RL",
                    LatLong = "51.055187, -4.193076",
                    DisabledAccess = true,
                    Info = "Disabled Access is via the Rear. You will need to contact a steward on arrival.",
                    Contact = "Bob Burns 01237 234567"
                },

                new Location()
                {
                    Id = 3,
                    Name = "Blue Lights Hall",
                    Address1 = "Vernons Lane",
                    Address2 = "Appledore",
                    PostCode = "EX39 1QU",
                    LatLong = "51.053881, -4.191394",
                    DisabledAccess = true,
                    Contact = "Charlie Collins 01237 345678"
                },

                new Location()
                {
                    Id = 4,
                    Name = "Baptist Chapel",
                    Address1 = "Meeting Street",
                    Address2 = "Appledore",
                    PostCode = "EX39 1RJ",
                    LatLong = "51.054130, -4.191909",
                    DisabledAccess = true,
                    Info = "Disabled Access is via the Rear. You will need to contact a steward on arrival.",
                    Contact = "David Davies 01237 456789"
                },

                new Location()
                {
                    Id = 5,
                    Name = "Library Room",
                    Address1 = "The Quay",
                    Address2 = "Appledore",
                    PostCode = "EX39 1QS",
                    LatLong = "51.052803, -4.190906",
                    DisabledAccess = false,
                    Info = "The Library Room is above the main library.",
                    Contact = "Edward Evans 01237 567890"
                },

                new Location()
                {
                    Id = 6,
                    Name = "Docton Court Gallery",
                    Address1 = "Myrtle Street",
                    Address2 = "Appledore",
                    PostCode = "EX39 1PH",
                    LatLong = "51.051868, -4.192217",
                    DisabledAccess = true,
                    Info = "Access is via a cobbled courtyard.",
                    Contact = "Frank Francis 01237 678901"
                },

                new Location()
                {
                    Id = 7,
                    Name = "Seagate Hotel",
                    Address1 = "The Quay",
                    Address2 = "Appledore",
                    PostCode = "EX39 1QS",
                    LatLong = "51.054371, -4.191034",
                    DisabledAccess = true,
                    Contact = "Geoff Graves 01237 789012"
                },

                new Location()
                {
                    Id = 8,
                    Name = "Community Hall",
                    Address1 = "New Quay Street",
                    Address2 = "Appledore",
                    PostCode = "EX39 1LU",
                    LatLong = "51.051539, -4.193112",
                    DisabledAccess = true,
                    Contact = "Harold Harris 01237 890123"
                },

                new Location()
                {
                    Id = 9,
                    Name = "Sandy Brown's Gallery",
                    Address1 = "Marine Parade",
                    Address2 = "Appledore",
                    PostCode = "EX39 1PJ",
                    LatLong = "51.051401, -4.192802",
                    DisabledAccess = false,
                    Contact = "Ian Ings 01237 901234"
                },

                new Location()
                {
                    Id = 10,
                    Name = "Suzie's Tea Room",
                    Address1 = "Market Street",
                    Address2 = "Appledore",
                    PostCode = "EX39 1PP",
                    LatLong = "51.053635, -4.191304",
                    DisabledAccess = true,
                    Info = "Please note Market Street is pedestrianised with no vehicle access.",
                    Contact = "Julie Jeffries 01237 012345"
                },

                new Location()
                {
                    Id = 11,
                    Name = "The Champ",
                    Address1 = "Meeting Street",
                    Address2 = "Appledore",
                    PostCode = "EX39 1RJ",
                    LatLong = "51.054158, -4.191527",
                    DisabledAccess = false,
                    Contact = "Kevin Keen 01237 111222"
                },

                new Location()
                {
                    Id = 12,
                    Name = "The Beaver",
                    Address1 = "Irsha Street",
                    Address2 = "Appledore",
                    PostCode = "EX39 1RY",
                    LatLong = "51.056924, -4.195024",
                    DisabledAccess = true,
                    Contact = "Lee Lemon 01237 222333"
                },

                new Location()
                {
                    Id = 13,
                    Name = "Kingsley School Hall",
                    Address1 = "Northdown Road",
                    Address2 = "Bideford",
                    PostCode = "EX39 3LY",
                    LatLong = "51.020673, -4.217464",
                    DisabledAccess = true,
                    Contact = "Mike Morris 01271 333444"
                },

                new Location()
                {
                    Id = 14,
                    Name = "The Royal George",
                    Address1 = "Irsha Street",
                    Address2 = "Appledore",
                    PostCode = "EX39 1RY",
                    LatLong = "51.057296, -4.195742",
                    DisabledAccess = true,
                    Info = "Regrettably there is no disabled access to the upper floor of the bar or restaurant.",
                    Contact = "Norman Norrris 01237 444555"
                }
            };

            //Event seed
            IList<Event> intialisedEvents = new List<Event>
            {
               new Event()
                {
                    Id = 1,
                    Date = new DateTime(2019,9,21),
                    StartTime = new DateTime(2019,9,21),
                    EndTime = new DateTime(2019,9,21),
                    Name = "La Vie en Rose" ,
                    Author = "Music",
                    Details =" Vestibulum at nunc sed metus congue sollicitudin",
                    Description = "Nullam accumsan eleifend eros ac egestas. Curabitur iaculis et turpis ut faucibus." +
                    " Curabitur ornare molestie nisi et sodales. Phasellus sem nulla, egestas sit amet justo nec, feugiat" +
                    " tempor ante. Vestibulum at nunc sed metus congue sollicitudin. Pellentesque molestie, eros et" +
                    " hendrerit mollis, massa dolor rutrum lorem, eget vulputate mauris lacus vitae justo. Donec vulputate" +
                    " eu sapien eu rhoncus. Aenean placerat faucibus tortor id pellentesque. Integer nec lectus a urna " +
                    "tincidunt tempus. ",
                    Capacity = 80,
                    TicketPrice = 10,
                    IsMemberOnly = false,
                    LocationId = 1,
                    ImageId = 1,
                    BookUrl ="Link goes here",
                    AuthorUrl="Link goes here",

                },

               new Event()
                {
                    Id = 2,
                    Date = new DateTime(2019,9,21),
                    StartTime = new DateTime(2019,9,21),
                    EndTime = new DateTime(2019,9,21),
                    Name = "Peter Rabbit" ,
                    Author = "Film",
                    Details =" Vestibulum at nunc sed metus congue sollicitudin",
                    Description = "Nullam accumsan eleifend eros ac egestas. Curabitur iaculis et turpis ut faucibus." +
                    " Curabitur ornare molestie nisi et sodales. Phasellus sem nulla, egestas sit amet justo nec, feugiat" +
                    " tempor ante. Vestibulum at nunc sed metus congue sollicitudin. Pellentesque molestie, eros et" +
                    " hendrerit mollis, massa dolor rutrum lorem, eget vulputate mauris lacus vitae justo. Donec vulputate" +
                    " eu sapien eu rhoncus. Aenean placerat faucibus tortor id pellentesque. Integer nec lectus a urna " +
                    "tincidunt tempus. ",
                    Capacity = 40,
                    TicketPrice = 8,
                    IsMemberOnly = false,
                    LocationId = 2,
                    ImageId = 1,
                    BookUrl ="Link goes here",
                    AuthorUrl="Link goes here",

                },

               new Event()
                {
                    Id = 3,
                    Date = new DateTime(2019,9,22),
                    StartTime = new DateTime(2019,9,22),
                    EndTime = new DateTime(2019,9,22),
                    Name = "One Sentence",
                    Author = "Jeremy Vine",
                    Details =" Vestibulum at nunc sed metus congue sollicitudin",
                    Description = "Nullam accumsan eleifend eros ac egestas. Curabitur iaculis et turpis ut faucibus." +
                    " Curabitur ornare molestie nisi et sodales. Phasellus sem nulla, egestas sit amet justo nec, feugiat" +
                    " tempor ante. Vestibulum at nunc sed metus congue sollicitudin. Pellentesque molestie, eros et" +
                    " hendrerit mollis, massa dolor rutrum lorem, eget vulputate mauris lacus vitae justo. Donec vulputate" +
                    " eu sapien eu rhoncus. Aenean placerat faucibus tortor id pellentesque. Integer nec lectus a urna " +
                    "tincidunt tempus. ",
                    Capacity = 230,
                    TicketPrice = 10,
                    IsMemberOnly = true,
                    LocationId = 3,
                    ImageId = 1,
                    BookUrl ="Link goes here",
                    AuthorUrl="Link goes here",

                },

               new Event()
                {
                    Id = 4,
                    Date = new DateTime(2019,9,23),
                    StartTime = new DateTime(2019,9,23),
                    EndTime = new DateTime(2019,9,23),
                    Name ="The Burning Chambers" ,
                    Author = "Kate Mosse" ,
                    Details =" Vestibulum at nunc sed metus congue sollicitudin",
                    Description = "Nullam accumsan eleifend eros ac egestas. Curabitur iaculis et turpis ut faucibus." +
                    " Curabitur ornare molestie nisi et sodales. Phasellus sem nulla, egestas sit amet justo nec, feugiat" +
                    " tempor ante. Vestibulum at nunc sed metus congue sollicitudin. Pellentesque molestie, eros et" +
                    " hendrerit mollis, massa dolor rutrum lorem, eget vulputate mauris lacus vitae justo. Donec vulputate" +
                    " eu sapien eu rhoncus. Aenean placerat faucibus tortor id pellentesque. Integer nec lectus a urna " +
                    "tincidunt tempus. ",
                    Capacity = 230,
                    TicketPrice = 12,
                    IsMemberOnly = false,
                    LocationId = 4,
                    ImageId = 1,
                    BookUrl ="Link goes here",
                    AuthorUrl="Link goes here",

               },

               new Event()
                {
                    Id = 5,
                    Date = new DateTime(2019,9,23),
                    StartTime = new DateTime(2019,9,23),
                    EndTime = new DateTime(2019,9,23),
                    Name = "Horrible Science Lab",
                    Author = "Nick Arnold",
                    Details =" Vestibulum at nunc sed metus congue sollicitudin",
                    Description = "Nullam accumsan eleifend eros ac egestas. Curabitur iaculis et turpis ut faucibus." +
                    " Curabitur ornare molestie nisi et sodales. Phasellus sem nulla, egestas sit amet justo nec, feugiat" +
                    " tempor ante. Vestibulum at nunc sed metus congue sollicitudin. Pellentesque molestie, eros et" +
                    " hendrerit mollis, massa dolor rutrum lorem, eget vulputate mauris lacus vitae justo. Donec vulputate" +
                    " eu sapien eu rhoncus. Aenean placerat faucibus tortor id pellentesque. Integer nec lectus a urna " +
                    "tincidunt tempus. ",
                    Capacity = 40,
                    TicketPrice = 5,
                    IsMemberOnly = false,
                    LocationId = 5,
                    ImageId = 1,
                    BookUrl ="Link goes here",
                    AuthorUrl="Link goes here",

                },

               new Event()
                {
                    Id = 6,
                    Date = new DateTime(2019,9,24),
                    StartTime = new DateTime(2019,9,24),
                    EndTime = new DateTime(2019,9,24),
                    Name = "The Appledore Walk",
                    Author = "David Carter",
                    Details =" Vestibulum at nunc sed metus congue sollicitudin",
                    Description = "Nullam accumsan eleifend eros ac egestas. Curabitur iaculis et turpis ut faucibus." +
                    " Curabitur ornare molestie nisi et sodales. Phasellus sem nulla, egestas sit amet justo nec, feugiat" +
                    " tempor ante. Vestibulum at nunc sed metus congue sollicitudin. Pellentesque molestie, eros et" +
                    " hendrerit mollis, massa dolor rutrum lorem, eget vulputate mauris lacus vitae justo. Donec vulputate" +
                    " eu sapien eu rhoncus. Aenean placerat faucibus tortor id pellentesque. Integer nec lectus a urna " +
                    "tincidunt tempus. ",
                    Capacity = 80,
                    TicketPrice = 10,
                    IsMemberOnly = true,
                    LocationId = 6,
                    ImageId = 1,
                    BookUrl ="Link goes here",
                    AuthorUrl="Link goes here",

                },

               new Event()
                {
                    Id = 7,
                    Date = new DateTime(2019,9,24),
                    StartTime = new DateTime(2019,9,24),
                    EndTime = new DateTime(2019,9,24),
                    Name = "The Dartmoor Conchies",
                    Author = "Simon Dartmoor",
                    Details =" Vestibulum at nunc sed metus congue sollicitudin",
                    Description = "Nullam accumsan eleifend eros ac egestas. Curabitur iaculis et turpis ut faucibus." +
                    " Curabitur ornare molestie nisi et sodales. Phasellus sem nulla, egestas sit amet justo nec, feugiat" +
                    " tempor ante. Vestibulum at nunc sed metus congue sollicitudin. Pellentesque molestie, eros et" +
                    " hendrerit mollis, massa dolor rutrum lorem, eget vulputate mauris lacus vitae justo. Donec vulputate" +
                    " eu sapien eu rhoncus. Aenean placerat faucibus tortor id pellentesque. Integer nec lectus a urna " +
                    "tincidunt tempus. ",
                    Capacity = 140,
                    TicketPrice = 6,
                    IsMemberOnly = false,
                    LocationId = 7,
                    ImageId = 1,
                    BookUrl ="Link goes here",
                    AuthorUrl="Link goes here",

                },

               new Event()
                 {
                        Id = 8,
                        Date = new DateTime(2019,9,25),
                        StartTime = new DateTime(2019,9,25),
                        EndTime = new DateTime(2019,9,25),
                        Name = "A Babies Bones",
                        Author = "Rebecca Alexander",
                        Details =" Vestibulum at nunc sed metus congue sollicitudin",
                        Description = "Nullam accumsan eleifend eros ac egestas. Curabitur iaculis et turpis ut faucibus." +
                        " Curabitur ornare molestie nisi et sodales. Phasellus sem nulla, egestas sit amet justo nec, feugiat" +
                        " tempor ante. Vestibulum at nunc sed metus congue sollicitudin. Pellentesque molestie, eros et" +
                        " hendrerit mollis, massa dolor rutrum lorem, eget vulputate mauris lacus vitae justo. Donec vulputate" +
                        " eu sapien eu rhoncus. Aenean placerat faucibus tortor id pellentesque. Integer nec lectus a urna " +
                        "tincidunt tempus. ",
                        Capacity = 40,
                        TicketPrice = 8,
                        IsMemberOnly = false,
                        LocationId = 8,
                        ImageId = 1,
                        BookUrl ="Link goes here",
                        AuthorUrl="Link goes here",

                 },

               new Event()
                  {
                        Id = 9,
                        Date = new DateTime(2019,9,25),
                        StartTime = new DateTime(2019,9,25),
                        EndTime = new DateTime(2019,9,25),
                        Name = "Playing with Clay",
                        Author = "Sandy Brown",
                        Details =" Vestibulum at nunc sed metus congue sollicitudin",
                        Description = "Nullam accumsan eleifend eros ac egestas. Curabitur iaculis et turpis ut faucibus." +
                        " Curabitur ornare molestie nisi et sodales. Phasellus sem nulla, egestas sit amet justo nec, feugiat" +
                        " tempor ante. Vestibulum at nunc sed metus congue sollicitudin. Pellentesque molestie, eros et" +
                        " hendrerit mollis, massa dolor rutrum lorem, eget vulputate mauris lacus vitae justo. Donec vulputate" +
                        " eu sapien eu rhoncus. Aenean placerat faucibus tortor id pellentesque. Integer nec lectus a urna " +
                        "tincidunt tempus. ",
                        Capacity = 7,
                        TicketPrice = 40,
                        IsMemberOnly = false,
                        LocationId = 9,
                        ImageId = 1,
                        BookUrl ="Link goes here",
                        AuthorUrl="Link goes here",

                  },

               new Event()
                   {
                        Id = 10,
                        Date = new DateTime(2019,9,26),
                        StartTime = new DateTime(2019,9,26),
                        EndTime = new DateTime(2019,9,26),
                        Name = "Ghost Walk 2",
                        Author = "Terry Bailey",
                        Details =" Vestibulum at nunc sed metus congue sollicitudin",
                        Description = "Nullam accumsan eleifend eros ac egestas. Curabitur iaculis et turpis ut faucibus." +
                        " Curabitur ornare molestie nisi et sodales. Phasellus sem nulla, egestas sit amet justo nec, feugiat" +
                        " tempor ante. Vestibulum at nunc sed metus congue sollicitudin. Pellentesque molestie, eros et" +
                        " hendrerit mollis, massa dolor rutrum lorem, eget vulputate mauris lacus vitae justo. Donec vulputate" +
                        " eu sapien eu rhoncus. Aenean placerat faucibus tortor id pellentesque. Integer nec lectus a urna " +
                        "tincidunt tempus. ",
                        Capacity = 30,
                        TicketPrice = 8,
                        IsMemberOnly = false,
                        LocationId = 10,
                        ImageId = 1,
                        BookUrl ="Link goes here",
                        AuthorUrl="Link goes here",

                   },

               new Event()
               {
                        Id = 11,
                        Date = new DateTime(2019,9,26),
                        StartTime = new DateTime(2019,9,26),
                        EndTime = new DateTime(2019,9,26),
                        Name = "The Salt Path",
                        Author = "Raymor Winn",
                        Details =" Vestibulum at nunc sed metus congue sollicitudin",
                        Description = "Nullam accumsan eleifend eros ac egestas. Curabitur iaculis et turpis ut faucibus." +
                        " Curabitur ornare molestie nisi et sodales. Phasellus sem nulla, egestas sit amet justo nec, feugiat" +
                        " tempor ante. Vestibulum at nunc sed metus congue sollicitudin. Pellentesque molestie, eros et" +
                        " hendrerit mollis, massa dolor rutrum lorem, eget vulputate mauris lacus vitae justo. Donec vulputate" +
                        " eu sapien eu rhoncus. Aenean placerat faucibus tortor id pellentesque. Integer nec lectus a urna " +
                        "tincidunt tempus. ",
                        Capacity = 220,
                        TicketPrice = 8,
                        IsMemberOnly = false,
                        LocationId = 11,
                        ImageId = 1,
                        BookUrl ="Link goes here",
                        AuthorUrl="Link goes here",

               },

               new Event()
               {
                    Id = 12,
                    Date = new DateTime(2019,9,27),
                    StartTime = new DateTime(2019,9,27),
                    EndTime = new DateTime(2019,9,27),
                    Name = "Beer Tasting",
                    Author = "Barry Raynes",
                    Details =" Vestibulum at nunc sed metus congue sollicitudin",
                    Description = "Nullam accumsan eleifend eros ac egestas. Curabitur iaculis et turpis ut faucibus." +
                   " Curabitur ornare molestie nisi et sodales. Phasellus sem nulla, egestas sit amet justo nec, feugiat" +
                   " tempor ante. Vestibulum at nunc sed metus congue sollicitudin. Pellentesque molestie, eros et" +
                   " hendrerit mollis, massa dolor rutrum lorem, eget vulputate mauris lacus vitae justo. Donec vulputate" +
                   " eu sapien eu rhoncus. Aenean placerat faucibus tortor id pellentesque. Integer nec lectus a urna " +
                   "tincidunt tempus. ",
                    Capacity = 40,
                    TicketPrice = 8,
                    IsMemberOnly = false,
                    LocationId = 12,
                    ImageId = 1,
                    BookUrl ="Link goes here",
                    AuthorUrl="Link goes here",

               },

               new Event()
               {
                   Id = 13,
                   Date = new DateTime(2019,9,27),
                   StartTime = new DateTime(2019,9,27),
                   EndTime = new DateTime(2019,9,27),
                   Name = "Dare to Try",
                   Author = "Louise Minchin",
                   Details =" Vestibulum at nunc sed metus congue sollicitudin",
                   Description = "Nullam accumsan eleifend eros ac egestas. Curabitur iaculis et turpis ut faucibus." +
                   " Curabitur ornare molestie nisi et sodales. Phasellus sem nulla, egestas sit amet justo nec, feugiat" +
                   " tempor ante. Vestibulum at nunc sed metus congue sollicitudin. Pellentesque molestie, eros et" +
                   " hendrerit mollis, massa dolor rutrum lorem, eget vulputate mauris lacus vitae justo. Donec vulputate" +
                   " eu sapien eu rhoncus. Aenean placerat faucibus tortor id pellentesque. Integer nec lectus a urna " +
                   "tincidunt tempus. ",
                   Capacity = 230,
                   TicketPrice = 10,
                   IsMemberOnly = false,
                   LocationId = 13,
                   ImageId = 1,
                   BookUrl ="Link goes here",
                   AuthorUrl="Link goes here",

               },
            };

            // Image seed
            Image initialisedImage = new Image
            {
                Id = 1,
                ImagePath = "~/Content/SiteImages/image-placeholder-350x350.png"
            };

            // MembershipType seed
            IList<MembershipType> initialisedMembershipTypes = new List<MembershipType> {


                new MembershipType
                {
                    Id = 1,
                    Type = "Non-Member",
                    Expiry = false,
                    Price = 0
                },

                new MembershipType
                {
                    Id = 2,
                    Type = "Standard",
                    Expiry = true,
                    Price = 20
                    
                },

                new MembershipType
                {
                    Id = 3,
                    Type = "Life Time",
                    Expiry = false,
                    Price = 0
                }
            };

            //Add-on seed
            IList<AddOn> initialisedAddOns = new List<AddOn>
            {
                new AddOn
                {
                    Id = 1,
                    Name= "Novelty Bag",
                    Description= "Novelty Peter Rabbit fabric bag signed by Beatrix Potter. White background with blue seams. 10L in size." ,
                    Quantity= 20,
                    Price= 15,
                    EventId= 2,
                },

                new AddOn
                {

                    Id = 2,
                    Name = "Appledore T-Shirt",
                    Description = "Brighten up your summer with this limited addition Appledore T-shirt. Sure to make all of your friends jealous.",
                    Quantity = 40,
                    Price = 20,
                    EventId = 7,
                },

                new AddOn
                {

                    Id = 3,
                    Name = "Novelty Beer Stein",
                    Description = "Signed by Barry himself, stand ut from the croud with this hand-made stein.",
                    Quantity = 15,
                    Price = 10,
                    EventId = 12,

                },

                new AddOn
                {

                    Id = 4,
                    Name = "Clay Making Kit",
                    Description = "Includes 3 500g blocks of clay, 300ml of Glaze and a colour pain pallet.",
                    Quantity = 4,
                    Price = 15,
                    EventId = 9,

                },

                new AddOn
                {

                    Id = 5,
                    Name = "Signed Book",
                    Description = "Get yourself a book signed by Jeremy Vine himself and a chance to visit him personally.",
                    Quantity = 90,
                    Price = 22,
                    EventId = 3,

                },

                new AddOn
                {

                    Id = 6,
                    Name = "Slime Making Kit",
                    Description = "Make your own slime! Includes several different colours.",
                    Quantity = 10,
                    Price = 12,
                    EventId = 5,
                },

             };

            IList<Customer> initialisedCustomers = new List<Customer>
            {
                new Customer
                {
                    Id = "1",
                    UserId= "admin",
                    Name= "Lucy Stalker",
                    Address1= "123 Avenue",
                    Address2= "London",
                    Address3= "",
                    PostCode= "EC123LZ",
                    Email= "Lucy.Stalker@test.com",
                    PhoneNumber= "123456123458" ,
                    MembershipTypeId = 2,
                },

                new Customer
                {
                    Id= "2",
                    UserId= "boxoffice",
                    Name= "Samantha Squire" ,
                    Address1= "12 Road Street",
                    Address2= "Sheffield",
                    Address3= "",
                    PostCode= "S215GN",
                    Email= "sammysquire@test.com",
                    PhoneNumber= "01149804582",
                    MembershipTypeId= 1,
                },

                new Customer
                {
                    Id= "3",
                    UserId= "user",
                    Name= "Marcus Pledge",
                    Address1= "1 Castle Lane",
                    Address2= "Floor 3",
                    Address3= "Doncaster",
                    PostCode= "D6JHS8",
                    Email= "the.pledge.m@test.com",
                    PhoneNumber= "01893920863",
                    MembershipTypeId = 2
                },

                new Customer
                {
                    Id= "4",
                    UserId= "4",
                    Name= "Amy White",
                    Address1= "52 Brooke Ln",
                    Address2= "Leeds",
                    Address3= "",
                    PostCode= "LS8709K",
                    Email= "a.white@test.com",
                    PhoneNumber= "01757889365",
                    MembershipTypeId= 1,
                },

                new Customer
                {
                    Id= "5",
                    UserId= "5",
                    Name= "Tinker Bell",
                    Address1= "12 Boat Line",
                    Address2= "Pixie Street",
                    Address3= "Neverland",
                    PostCode= "NL89HK2",
                    Email= "Tinkerbell@test.com",
                    PhoneNumber= "7777778990",
                    MembershipTypeId= 1,
                },

                new Customer
                {
                    Id= "6",
                    UserId= "6",
                    Name= "Peter Pahn",
                    Address1= "12 Boat Line",
                    Address2= "Pixie Street",
                    Address3= "Neverland",
                    PostCode= "NL89HK2",
                    Email= "PeterPan@test.com",
                    PhoneNumber= "666789219274",
                    MembershipTypeId= 1,
                },

                new Customer
                {
                    Id= "7",
                    UserId= "7",
                    Name= "Andrew Cole",
                    Address1= "67 Walkers Lane",
                    Address2= "Selby",
                    Address3= "North Yorkshire",
                    PostCode= "YO89KO",
                    Email= "a.cole@test.com",
                    PhoneNumber= "01787639223" ,
                    MembershipTypeId= 1,
                },

                new Customer

                {
                    Id= "8",
                    UserId= "8",
                    Name= "Eenie Miney" ,
                    Address1= "2 Mousley Close",
                    Address2= "Edinborough",
                    Address3= "",
                    PostCode= "EH13LK",
                    Email= "Eenie.meenie.miney.m@tst.com",
                    PhoneNumber= "123490987768",
                    MembershipTypeId= 1,
                },

                new Customer
                {
                    Id= "9",
                    UserId= "9",
                    Name= "Dave Bob",
                    Address1= "23 Robley Lane",
                    Address2= "Sheffield",
                    Address3= "",
                    PostCode= "S29OL",
                    Email= "Dave.bb@test.com",
                    PhoneNumber= "88975461209",
                    MembershipTypeId= 2,
                },

                new Customer
                {
                    Id= "10",
                    UserId= "10",
                    Name= "Chelsea Lou",
                    Address1= "19 Floral Path",
                    Address2= "Sheffield",
                    Address3= "",
                    PostCode= "S18RF",
                    Email= "chelsea.lou@test.com",
                    PhoneNumber= "0985672983",
                    MembershipTypeId= 1,
                },
            };

            context.MembershipTypes.AddRange(initialisedMembershipTypes);
            context.Images.Add(initialisedImage);
            context.Locations.AddRange(initialisedLocations);
            context.Events.AddRange(intialisedEvents);
            context.AddOns.AddRange(initialisedAddOns);
            context.Customers.AddRange(initialisedCustomers);
            context.SaveChanges();

            base.Seed(context);
        }
    }
}
