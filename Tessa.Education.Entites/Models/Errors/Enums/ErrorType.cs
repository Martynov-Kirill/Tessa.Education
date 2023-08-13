
using System.ComponentModel;

// ReSharper disable InconsistentNaming

namespace Tessa.Education.Entites.Models.Errors.Enums
{
    /// <summary>
    /// Error table auto generated enumeration
    /// </summary>
    public enum ErrorType
    {
          
        [Description("Непредвиденная ошибка; свяжитесь со службой технической поддержки.")]
        GEN_ERR = 1,

      
        [Description("Что то пошло не так; свяжитесь со службой технической поддержки.")]
        UNK_ERR = 2,

      
        [Description("Ошибка данныx; Данные не верны или пустые {1}.")]
        DATA_ERR = 3,

      
        [Description("Ошибка. Обьект не найден или пустой; {1}.")]
        OBJ_NOT_FND = 4,

      
        [Description("Поле '{0}' пустое")]
        FIELD_IS_EMPTY = 5,

      
        [Description("Ошибка поиска описание ошибки: MnemonicCode - {0}, Source - {1}, Message - {2}")]
        FIND_MNEMONICCODE_ERR = 6,

      
        [Description("Файл пустой")]
        FILE_IS_EMPTY = 7
  
    }
}
