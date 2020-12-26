namespace SightsView.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore.Internal;
    using SightsView.Data.Models;

    public class CategoriesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Categories.Any())
            {
                return;
            }

            var allCategories = new List<Category>
            {
                new Category()
                {
                    Name = "Abstract",
                    Description = "Abstract photography is often compared to abstract art as it also focuses on the shape, pattern, color, form, and texture. The subject of abstract photography is generally second to be viewed as the impact of the aspects surrounding the subject convey the essence of the image in most cases.",
                },
                new Category()
                {
                    Name = "Aerial",
                    Description = "Aerial photography is that kind of photography where pictures are captures from a higher altitude such as planes, air balloons, parachutes, drones and skyscrapers. These pictures provide a larger view of the subject and its background.",
                },
                new Category()
                {
                    Name = "Architectural",
                    Description = "This type of photography deals with taking shots of structures, houses and buildings from different angles.",
                },
                new Category()
                {
                    Name = "Astro",
                    Description = "Photography of astronomical bodies and celestial events including stars, the moon, the sun, planets, asteroids, and galaxies.",
                },
                new Category()
                {
                    Name = "Beauty",
                    Description = "Beauty photography focuses on bringing out the real beauty of the subjects and requires great talent and skill from the photographer. In order to capture the best beauty photographs, the proper combination of light along with complete trust and imagination from both the photographer and the model are absolutely essential.",
                },
                new Category()
                {
                    Name = "Bird",
                    Description = "Bird photography is one of the oldest and most popular types of photography that has been pursued by both amateur and skilled professionals. Bird photography requires extreme patience and skill on the part of the photographers as these feathered creatures are extremely moody in maintaining their position and pose.",
                },
                new Category()
                {
                    Name = "Black and White",
                    Description = "The trend of capturing pictures in black and white is popular even today as it helps to bring out the natural beauty of the subjects. This type of photography makes extensive use of contrasts and shadows to give the pictures a realistic and beautiful look.",
                },
                new Category()
                {
                    Name = "Bodyshape",
                    Description = "Bodyscape photography is the art of clicking images of nude human subjects in a manner that is not erotic yet is mesmerizing. This type of photography is generally carried out in black and white in a environment where the natural elements can be easily integrated into the pictures.",
                },
                new Category()
                {
                    Name = "Candid",
                    Description = "Candid photography comprises of pictures clicked in completely natural states of the subjects without any prior preparedness. This type of photography is increasingly becoming popular not only during social events and special occasions, but for general images as well as it helps in capturing the uniqueness and magic of the moment.",
                },
                new Category()
                {
                    Name = "Conceptual",
                    Description = "Conceptual photography is all about presenting a concept or an idea present in the mind of the photographer to other people only through the medium of photographs. This type of photography is generally used in advertising, where a picture helps in reiterating an idea, a tagline or a catchphrase for a product or service.",
                },
                new Category()
                {
                    Name = "Event",
                    Description = string.Empty,
                },
                new Category()
                {
                    Name = "Family",
                    Description = "The different expressions of a baby as well as along with the family members are captured in this type of photography. The whole family comes together to freeze one moment in time in this type of photography.",
                },
                new Category()
                {
                    Name = "Fashion",
                    Description = "Fashion photography captures models in a glamorous light display fashion items such as clothes, shoes and other accessories. This type of photography is conducted mostly for advertisements and fashion magazines.",
                },
                new Category()
                {
                    Name = "Fireworks",
                    Description = "As the name indicates, Firework photography deals with clicking images of the beautiful firework display especially at night. This type of photography is relatively easy and can be carried on with a normal camera. These images, when clicked in a proper manner are quite majestic and appealing for the viewers.",
                },
                new Category()
                {
                    Name = "Food",
                    Description = "Food photography is the art of clicking images of various food items in manner that makes it immediately appealing to the viewers. The food photographers need to pay attention to not only the proper arrangement of the food but also the context in which it is to be presented to get the best shots.",
                },
                new Category()
                {
                    Name = "Forced Perspective",
                    Description = "This type of photography uses optical illusion to make an object look closer or farther or bigger or smaller than it actually is. The photography manipulates the visual perception of human eye to create fun images that easily attract viewers besides conveying important information in some cases.",
                },
                new Category()
                {
                    Name = "HDR",
                    Description = "HDR photography is one of the most creative types of photography and requires extreme skill on part of the photographers. It is created by blending three different types of pictures into a single picture that highlights the amazing contrast of the individual images to create a truly appealing look.",
                },
                new Category()
                {
                    Name = "High Speed",
                    Description = "High speed photography is the art of capturing the images of events that take place at a rapid pace. It is an extremely exciting and somewhat complicated type of photography that enables photographers to depict the very fast phenomenon one frame at a time and thus highlight their beauty.",
                },
                new Category()
                {
                    Name = "Infra Red",
                    Description = "This photography is generally used as a means of exploring the unseen aspects within an environment, by the photographers. The images are clicked using special equipment that is sensitive to the infrared light and not the visible light spectrum that helps them to see objects normally.",
                },
                new Category()
                {
                    Name = "Landscape",
                    Description = "If you love photography and have the tendency to stop and look at the beautiful scenery around you and freeze nature in one picture, you are definitely a type of photographer interested in the type of photography called landscape photography.",
                },
                new Category()
                {
                    Name = "Lomo",
                    Description = "This is special genre of photography in which the pop images are captures using the quirky cameras manufactured by an Austrian company called Lomo. Lomo photography is considered to be a spontaneous and candid brand of photography carried out with technically poor cameras.",
                },
                new Category()
                {
                    Name = "Long Exposure",
                    Description = "This genre of photography adds magical effects to the pictures, which is why it is often associated with fine art photography. It is carried out using a long duration shutter speed that helps to capture the stationary elements of an image in a sharp manner while adjusting the moving ones.",
                },
                new Category()
                {
                    Name = "Macro",
                    Description = "Macro photography is that type of photography in which pictures are shot at a closer range to showcase the details of the subject matter. The interesting subjects of macro photography are flowers, insects, textures of interwoven things such as sweaters, baskets etc.",
                },
                new Category()
                {
                    Name = "Micro",
                    Description = "This is a special genre of photography, wherein the images are clicked with the help of a microscope. Micro photography plays an important role in medical research for understanding the physical traits and features of organisms that are too small to be seen by naked eye.",
                },
                new Category()
                {
                    Name = "Mobile",
                    Description = "This genre of photography came into existence when cameras were first integrated into mobile phones. Today it is one of the most popular types of photography especially in view of the fact that mobile phone cameras today offer the precision and clarity of professional equipment.",
                },
                new Category()
                {
                    Name = "Nature",
                    Description = "Natural photography comprises of pictures of nature as viewed from the eyes of the photographer. Contrary to what many people believe, nature photography is not only restricted to capturing the images of trees and plants, but rather includes any outdoor natural aspect including hills, water bodies and even the sky.",
                },
                new Category()
                {
                    Name = "Night",
                    Description = "Contrary to what most people think, night presents and unique beauty and challenge for photographers. Night photography not only offers n entirely different perspective of things but also provides a great depth of colors that further enhances the magic and mystery of clicking images at night.",
                },
                new Category()
                {
                    Name = "Nude",
                    Description = "Nude photography is a genre of fine art photography in which the subjects are generally in a nude or semi-nude position. Also popular by the name of erotic photography, the images clicked under this genre focus on the aesthetic qualities of the subject, including its form, emotional aspect and composition.",
                },
                new Category()
                {
                    Name = "Other",
                    Description = string.Empty,
                },
                new Category()
                {
                    Name = "Pet",
                    Description = string.Empty,
                },
                new Category()
                {
                    Name = "Portrait",
                    Description = "One of the oldest types of photography is portrait photography. It can range from shooting your family members to friends to pets. It is often called portraiture and this type of photographer abounds.",
                },
                new Category()
                {
                    Name = "Real Estate",
                    Description = "With the ever increasing competition in the real estate sector, the trend of real estate photography is fast catching up as a mean to attract prospective clients. This photography is generally utilized by real estate agents and developers to highlight the specific features of a property for buyers to investors.",
                },
                new Category()
                {
                    Name = "Reflection",
                    Description = "Reflection photography is extremely challenging and can be carried out only by professional photographers having a thorough understanding of angles, perspectives and various other factors. When carried out in a proper manner, this type of photography can provide stunning results in the form of visually enchanting images.",
                },
                new Category()
                {
                    Name = "Sports",
                    Description = "This genre of photography specializes in capturing a decisive moment in an event of sports.",
                },
                new Category()
                {
                    Name = "Storm",
                    Description = "While storms are generally considered to be disastrous and dangerous, they can look extremely beautiful when captured by a skilled storm photographer. Storm photographers can generate a sense of awe, fear and respect for this strong force of nature all at the same time through the images captured by them.",
                },
                new Category()
                {
                    Name = "Street",
                    Description = "Street photography is generally use to reflect the everyday life trends prevalent in the society and is used for showing a mirror to the people. In most cases the images that comprise street photography are considered ironic and distanced from the subject in some manner.",
                },
                new Category()
                {
                    Name = "Time Lapse",
                    Description = "This is the type of photography that is used for capturing the subjects while they are in motion. To get the best shots, photographers practicing this type of photography need to have the patience and the skill to shoot the subjects at different time intervals in a continuous manner.",
                },
                new Category()
                {
                    Name = "Travel",
                    Description = "This type of photography is generally used for showcasing the beauty or important features of specific locations. This is one of the most exciting types of photography as photographers can enjoy the life and culture of different locations while capturing the images with their camera.",
                },
                new Category()
                {
                    Name = "Underwater",
                    Description = "As the name indicates, this type of photography involves capturing pictures underwater with the help of water tight camera housing. The photographers need to spend a good time exploring the depths of water to capture the unique beauty of nature hidden below the seemingly calm water surface.",
                },
                new Category()
                {
                    Name = "Vehicle",
                    Description = "Vehicle Photography is the art of clicking beautiful and amazing pictures of vehicles. It usually requires the photographers to capture the finer details of both the interior and exterior of the vehicle from different angles, as these images are generally used for promotion of the automobile by highlighting its features.",
                },
                new Category()
                {
                    Name = "Vintage",
                    Description = "This type of photography is chosen to click images that are meant to retain an aged look. In the recent years there has been a significant growth in the demand for vintage photography as these images hold a unique charm and appeal that makes them truly classic.",
                },
                new Category()
                {
                    Name = "Wedding",
                    Description = "A person dealing in this type of photography has to be an expert in portraiture and extremely good editing skills. The demand for wedding photography or event photography is more.",
                },
                new Category()
                {
                    Name = "Wildlife",
                    Description = "The genre of photography that focuses on animals and their natural habitat is called wildlife photography. Animal behaviors in wild are also capture by wildlife photographer.",
                }
            };

            await dbContext.Categories.AddRangeAsync(allCategories);
        }
    }
}
