﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GTA;
using GTA.Native;
using GTA.Math;

namespace vco
{
    public class VCOFunctions
    {
        public VCOFunctions()
        {
            return;
        }

        public static int GetHashKey(string value)
        {    
            return Function.Call<int>(Hash.GET_HASH_KEY, value);
        }

        public static bool IsVCOLoaded()
        {
            var vcoDLC = GetHashKey("spvco"); // We are storing this information for later use. No need to use more computer power to constantly get the hash key of the dlc.

            if (Function.Call<bool>(Hash.IS_DLC_PRESENT, vcoDLC))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public class UI
        {
            public static void ContactMessage(string icon, int type, string sender, string title, string text)
            {
                Function.Call(Hash.BEGIN_TEXT_COMMAND_THEFEED_POST, "STRING");
                Function.Call(Hash.ADD_TEXT_COMPONENT_SUBSTRING_PLAYER_NAME, text);
                Function.Call(Hash.END_TEXT_COMMAND_THEFEED_POST_MESSAGETEXT, icon, icon, true, type, sender, title, text);
                Function.Call(Hash.END_TEXT_COMMAND_THEFEED_POST_TICKER, false, true);
                Function.Call(Hash.PLAY_SOUND_FRONTEND, -1, "Text_Arrive_Tone", "Phone_SoundSet_Default");
            }

            public static void TextBox(string text, bool looptext, bool beepsound, int duration)
            {
                Function.Call(Hash.BEGIN_TEXT_COMMAND_DISPLAY_HELP, "STRING");
                Function.Call(Hash.ADD_TEXT_COMPONENT_SUBSTRING_PLAYER_NAME, text);
                Function.Call((Hash)0x238FFE5C7B0498A6, 0, looptext, beepsound, duration); // The hash being used here is: END_TEXT_COMMAND_DISPLAY_HELP
            }

            public static void CreateBlip(Vector3 pos, string blipName, int spriteID, bool shortRange, int colourID, List<Blip> list)
            {
                Blip b = World.CreateBlip(pos);
                Function.Call(Hash.SET_BLIP_SPRITE, b, spriteID); // You can get it ID here: https://docs.fivem.net/docs/game-references/blips/
                Function.Call(Hash.SET_BLIP_AS_SHORT_RANGE, b, shortRange); // True or false on whether the blips will appear on minimap when far away
                Function.Call(Hash.SET_BLIP_COLOUR, b, colourID); // You can get the ID here: https://runtime.fivem.net/doc/natives/#_0x03D7FB09E75D6B7E 0 = white, most of the time you will be using that.
                Function.Call(Hash.BEGIN_TEXT_COMMAND_SET_BLIP_NAME, "STRING");
                Function.Call((Hash)0x6C188BE134E074AA, blipName); // Hash being used here is: ADD_TEXT_COMPONENT_SUBSTRING_PLAYER_NAME 
                Function.Call(Hash.END_TEXT_COMMAND_SET_BLIP_NAME, b);
                list.Add(b);
            }
        }
    }
}
