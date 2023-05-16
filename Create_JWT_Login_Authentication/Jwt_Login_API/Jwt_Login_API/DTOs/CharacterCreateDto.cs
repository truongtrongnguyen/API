namespace Jwt_Login_API.DTOs
{
    public record struct CharacterCreateDto(string Name,
                                            BackpackCreateDto Backpack,
                                            List<WeaponCreateDto> WeaponCreateDtos,
                                            List<FactionCreateDto> FactionCreateDtos
                                            );
}
