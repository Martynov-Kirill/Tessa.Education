using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Tessa.Education.Entites.Entities.Interfaces;

namespace Tessa.Education.Entites.Entities
{
    public abstract class Entity : IEntity
    {
        /// <inheritdoc />
        [JsonIgnore]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key()]
        public int Id { get; set; }
    }
}
