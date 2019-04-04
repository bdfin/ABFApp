﻿using System;
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
                    Id = 0,
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
                    Id = 1,
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
                    Id = 2,
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
                    Id = 3,
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
                    Id = 4,
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
                    Id = 5,
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
                    Id = 6,
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
                    Id = 7,
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
                    Id = 8,
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
                    Id = 9,
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
                    Id = 10,
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
                    Id = 11,
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
                    Id = 12,
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
                    Id = 13,
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
                    Id = 0,
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
                    LocationId = 0,
                    ImageId = 1,
                    BookUrl ="Link goes here",
                    AuthorUrl="Link goes here",

                },

               new Event()
                {
                    Id = 1,
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
                    LocationId = 1,
                    ImageId = 1,
                    BookUrl ="Link goes here",
                    AuthorUrl="Link goes here",

                },

               new Event()
                {
                    Id = 2,
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
                    LocationId = 2,
                    ImageId = 1,
                    BookUrl ="Link goes here",
                    AuthorUrl="Link goes here",

                },

               new Event()
                {
                    Id = 3,
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
                    LocationId = 4,
                    ImageId = 1,
                    BookUrl ="Link goes here",
                    AuthorUrl="Link goes here",

                },

               new Event()
                {
                    Id = 5,
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
                    LocationId = 6,
                    ImageId = 1,
                    BookUrl ="Link goes here",
                    AuthorUrl="Link goes here",

                },

                new Event()
                 {
                        Id = 7,
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
                        LocationId = 8,
                        ImageId = 1,
                        BookUrl ="Link goes here",
                        AuthorUrl="Link goes here",

                  },

                new Event()
                   {
                        Id = 9,
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
                        LocationId = 10,
                        ImageId = 1,
                        BookUrl ="Link goes here",
                        AuthorUrl="Link goes here",

               },

                new Event()
               {
                    Id = 11,
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
                   LocationId = 12,
                   ImageId = 1,
                   BookUrl ="Link goes here",
                   AuthorUrl="Link goes here",

               }
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
                    Type = "Standard",
                    Expiry = true,
                    Price = 20.00
                    
                },

                new MembershipType
                {
                    Id = 2,
                    Type = "Life Time",
                    Expiry = false,
                    Price = 0.00
                }
            };

            context.MembershipTypes.AddRange(initialisedMembershipTypes);
            context.Images.Add(initialisedImage);
            context.Locations.AddRange(initialisedLocations);
            context.Events.AddRange(intialisedEvents);
            context.SaveChanges();

            base.Seed(context);
        }
    }
}
