using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Jwt_Login_API.Models
{
    public class Weapon
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CharacterId { get; set; }
        [ForeignKey(nameof(CharacterId))]
        [JsonIgnore]
        public Character Character { get; set; }
    }
}
