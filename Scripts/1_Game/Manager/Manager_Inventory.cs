using System;
using System.Collections.Generic;
using System.Text;

public class Manager_Inventory : Singleton<Manager_Inventory>
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public string GetValueText(DataItem data, EItemValueType itemValueType)
    {
        //
        var temp = string.Empty;

        //
        switch (itemValueType)
        {
            case EItemValueType.Name:
                temp = Manager_UI.Instance.GetTextItem(data.pTableInfo.name);                
                break;
            case EItemValueType.Grade:
                var key = 0;
                switch (data.pTableInfo.grade)
                {
                    case EGrade.Normal: key = 9000054; break;
                    case EGrade.Rare  : key = 9000055; break;
                    case EGrade.Elite : key = 9000056; break;
                    case EGrade.Epic  : key = 9000057; break;
                    case EGrade.Legend: key = 9000058; break;
                }
                temp = Manager_UI.Instance.GetTextCommon(key);
                break;
        }

        //
        return temp;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public string GetValueText(DataArmor data, EItemValueType itemValueType)
    {
        //
        var temp = string.Empty;

        //
        switch (itemValueType)
        {
            case EItemValueType.Name:
                temp = Manager_UI.Instance.GetTextArmor(data.pTableInfo.name);
                break;
            case EItemValueType.Grade:
                var key = 0;
                switch (data.pTableInfo.grade)
                {
                    case EGrade.Normal: key = 9000054; break;
                    case EGrade.Rare  : key = 9000055; break;
                    case EGrade.Elite : key = 9000056; break;
                    case EGrade.Epic  : key = 9000057; break;
                    case EGrade.Legend: key = 9000058; break;
                }
                temp = Manager_UI.Instance.GetTextCommon(key);
                break;
            case EItemValueType.Level:
                temp = string.Format(Manager_UI.Instance.GetTextCommon(9000039), data.pLevel);
                break;
        }

        //
        return temp;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public string GetValueText(DataWeapon data, EItemValueType itemValueType)
    {
        //
        var temp = string.Empty;

        //
        switch (itemValueType)
        {
            case EItemValueType.Name:
                temp = Manager_UI.Instance.GetTextWeapon(data.pTableInfo.name);
                break;
            case EItemValueType.Grade:
                var key = 0;
                switch (data.pTableInfo.grade)
                {
                    case EGrade.Normal: key = 9000054; break;
                    case EGrade.Rare  : key = 9000055; break;
                    case EGrade.Elite : key = 9000056; break;
                    case EGrade.Epic  : key = 9000057; break;
                    case EGrade.Legend: key = 9000058; break;
                }
                temp = Manager_UI.Instance.GetTextCommon(key);
                break;
            case EItemValueType.Level:
                temp = string.Format(Manager_UI.Instance.GetTextCommon(9000039), data.pLevel);
                break;
        }

        //
        return temp;
    }
}
