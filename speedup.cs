using System;

using ScrollsModLoader.Interfaces;
using UnityEngine;
using Mono.Cecil;
//using Mono.Cecil;
//using ScrollsModLoader.Interfaces;

using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Reflection;
using JsonFx.Json;
using System.Text.RegularExpressions;


namespace speedup.mod
{
    public class classdata {
        public double pointer;

        public GUISkin lobbySkin;
        public GUISkin cardListPopupSkin;
        public GUISkin cardListPopupGradientSkin;
        public GUISkin cardListPopupBigLabelSkin;
        public GUISkin cardListPopupLeftButtonSkin;
        public Rect outerRect;
        public Rect innerBGRect;
        public Rect innerRect;
        public Rect buttonLeftRect;
        public Rect buttonRightRect;
        public bool selectable;
        public Vector2 scrollPos;
        public float BOTTOM_MARGIN_EXTRA = (float)Screen.height * 0.047f;
        public Vector4 margins;
        public float bottomMargin = 0.08f;
        public List<Card> cards;
        public List<Card> selectedCards = new List<Card>();
        public ICardListCallback callback;
        public GUIContent buttonLeftContent;
        public GUIContent buttonRightContent;
        public Texture itemButtonTexture;
        public bool leftButtonEnabled;
        public bool rightButtonEnabled;
        public bool showFrame;
        public float scrollBarSize = 20f;
        public bool leftButtonHighlighted;
        public bool rightButtonHighlighted;
        public bool leftHighlightable;
        public bool rightHighlightable;
        public Texture2D bgBar;
        public float fieldHeight;
        public float costIconSize;
        public float costIconWidth;
        public float costIconHeight;
        public float cardHeight;
        public float cardWidth;
        public float labelX;
        public float labelsWidth;
        public int maxCharsName;
        public int maxCharsRK;
        public float offX;
        public float opacity;
        public bool clickableItems;
    
    
    }



    public class speedup : BaseMod
	{

        //List<classdata> cardlistpopo= new List<classdata>();
        Dictionary<CardListPopup, classdata> cardlists = new Dictionary<CardListPopup, classdata>();
        Dictionary<int, Texture> cardtextures = new Dictionary<int, Texture>();
        Texture icon_order = ResourceManager.LoadTexture("BattleUI/battlegui_icon_order");
        Texture icon_energy = ResourceManager.LoadTexture("BattleUI/battlegui_icon_energy");
        Texture icon_decay = ResourceManager.LoadTexture("BattleUI/battlegui_icon_decay");
        Texture icon_growth = ResourceManager.LoadTexture("BattleUI/battlegui_icon_growth");

        Texture button_cb_checked = ResourceManager.LoadTexture("Arena/scroll_browser_button_cb_checked");
        Texture button_cb = ResourceManager.LoadTexture("Arena/scroll_browser_button_cb");
        
        Texture[] zahlenarray = new Texture[10];
		//initialize everything here, Game is loaded at this point
        public speedup()
        {
            Console.WriteLine("SPEEED loadet");
            for (int i = 0; i < zahlenarray.Length; i++)
            {
               zahlenarray[i]= ResourceManager.LoadTexture("Scrolls/yellow_" + i.ToString());
            }

            
		}



		public static string GetName ()
		{
            return "speedthingsup";
		}

		public static int GetVersion ()
		{
			return 1;
		}

        private void addall(CardListPopup infotarget)
        {
            classdata dis = cardlists[infotarget];
            // (CardListPopup)typeof(TradeSystem).GetField("clInventoryP1", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(infotarget);
            
            dis.lobbySkin = (GUISkin)Resources.Load("_GUISkins/Lobby");

            
            dis.cardListPopupSkin = (GUISkin)Resources.Load("_GUISkins/CardListPopup");
            dis.cardListPopupGradientSkin = (GUISkin)Resources.Load("_GUISkins/CardListPopupGradient");
            dis.cardListPopupBigLabelSkin = (GUISkin)Resources.Load("_GUISkins/CardListPopupBigLabel");
            dis.cardListPopupLeftButtonSkin = (GUISkin)Resources.Load("_GUISkins/CardListPopupLeftButton");
            dis.outerRect = (Rect)typeof(CardListPopup).GetField("outerRect", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(infotarget);
            dis.innerBGRect = (Rect)typeof(CardListPopup).GetField("innerBGRect", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(infotarget);
            dis.innerRect = (Rect)typeof(CardListPopup).GetField("innerRect", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(infotarget);
            dis.buttonLeftRect = (Rect)typeof(CardListPopup).GetField("buttonLeftRect", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(infotarget);
            dis.buttonRightRect = (Rect)typeof(CardListPopup).GetField("buttonRightRect", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(infotarget);
            dis.selectable = (bool)typeof(CardListPopup).GetField("selectable", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(infotarget);
	        dis.BOTTOM_MARGIN_EXTRA=(float)Screen.height * 0.047f;
            dis.margins = (Vector4)typeof(CardListPopup).GetField("margins", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(infotarget);
            dis.bottomMargin= 0.08f;

            dis.cards = (List<Card>)typeof(CardListPopup).GetField("cards", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(infotarget);
            dis.selectedCards = (List<Card>)typeof(CardListPopup).GetField("selectedCards", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(infotarget);
            dis.callback = (ICardListCallback)typeof(CardListPopup).GetField("callback", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(infotarget);
            dis.buttonLeftContent = (GUIContent)typeof(CardListPopup).GetField("buttonLeftContent", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(infotarget);
            dis.buttonRightContent = (GUIContent)typeof(CardListPopup).GetField("buttonRightContent", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(infotarget);
            dis.itemButtonTexture = (Texture)typeof(CardListPopup).GetField("itemButtonTexture", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(infotarget);
            dis.leftButtonEnabled = (bool)typeof(CardListPopup).GetField("leftButtonEnabled", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(infotarget);
            dis.rightButtonEnabled = (bool)typeof(CardListPopup).GetField("rightButtonEnabled", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(infotarget);
            dis.showFrame = (bool)typeof(CardListPopup).GetField("showFrame", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(infotarget);
            dis.scrollBarSize= 20f;

            dis.leftButtonHighlighted = (bool)typeof(CardListPopup).GetField("leftButtonHighlighted", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(infotarget);
            dis.rightButtonHighlighted = (bool)typeof(CardListPopup).GetField("rightButtonHighlighted", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(infotarget);
            dis.leftHighlightable = (bool)typeof(CardListPopup).GetField("leftHighlightable", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(infotarget);
            dis.rightHighlightable = (bool)typeof(CardListPopup).GetField("rightHighlightable", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(infotarget);
            dis.bgBar = (Texture2D)typeof(CardListPopup).GetField("bgBar", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(infotarget);
            dis.fieldHeight = (float)typeof(CardListPopup).GetField("fieldHeight", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(infotarget);
            dis.costIconSize = (float)typeof(CardListPopup).GetField("costIconSize", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(infotarget);
            dis.costIconWidth = (float)typeof(CardListPopup).GetField("costIconWidth", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(infotarget);
            dis.costIconHeight = (float)typeof(CardListPopup).GetField("costIconHeight", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(infotarget);
            dis.cardHeight = (float)typeof(CardListPopup).GetField("cardHeight", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(infotarget);
            dis.cardWidth = (float)typeof(CardListPopup).GetField("cardWidth", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(infotarget);
            dis.labelX = (float)typeof(CardListPopup).GetField("labelX", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(infotarget);
            dis.labelsWidth = (float)typeof(CardListPopup).GetField("labelsWidth", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(infotarget);
            dis.maxCharsName = (int)typeof(CardListPopup).GetField("maxCharsName", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(infotarget);
            dis.maxCharsRK = (int)typeof(CardListPopup).GetField("maxCharsRK", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(infotarget);
            dis.offX = (float)typeof(CardListPopup).GetField("offX", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(infotarget);
            dis.opacity = (float)typeof(CardListPopup).GetField("opacity", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(infotarget);
            dis.clickableItems = (bool)typeof(CardListPopup).GetField("clickableItems", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(infotarget);
        }

        private void Rendercostss(Rect rect, Card card)
        {
            

                int value = 0;
                Texture texture = null;
                if (card.getCostOrder() > 0)
                {
                    value = card.getCostOrder();
                    texture = icon_order;
                }
                else
                {
                    if (card.getCostEnergy() > 0)
                    {
                        value = card.getCostEnergy();
                        texture = icon_energy;
                    }
                    else
                    {
                        if (card.getCostDecay() > 0)
                        {
                            value = card.getCostDecay();
                            texture = icon_decay;
                        }
                        else
                        {
                            if (card.getCostGrowth() > 0)
                            {
                                value = card.getCostGrowth();
                                texture = icon_growth;
                            }
                        }
                    }
                }
                if (texture != null)
                {
                    GUI.DrawTexture(rect, texture);
                    char[] array = Convert.ToString(value).ToCharArray();
                    for (int i = 0; i < array.Length; i++)
                    {
                        Rect position = new Rect(rect.xMax + 5f - (float)(array.Length - i) * rect.height * 0.6f, rect.y + 1f, rect.height * 0.7f, rect.height);
                        Texture image = zahlenarray[(int)Char.GetNumericValue(array[i])];
                        GUI.DrawTexture(position, image);
                    }
                }
            
        }

		//only return MethodDefinitions you obtained through the scrollsTypes object
		//safety first! surround with try/catch and return an empty array in case it fails
		public static MethodDefinition[] GetHooks (TypeDefinitionCollection scrollsTypes, int version)
		{
            try
            {
                return new MethodDefinition[] {
                    
                    scrollsTypes["CardListPopup"].Methods.GetMethod("Start")[0],
                    scrollsTypes["CardListPopup"].Methods.GetMethod("Update")[0],
                    scrollsTypes["CardListPopup"].Methods.GetMethod("Init", new Type[]{typeof(Rect),typeof(bool),typeof(bool),typeof(List<Card>),typeof(ICardListCallback),typeof(GUIContent),typeof(GUIContent),typeof(bool),typeof(bool),typeof(bool),typeof(bool),typeof(Texture),typeof(bool)}),
                     scrollsTypes["CardListPopup"].Methods.GetMethod("SetCardList", new Type[]{typeof(List<Card>)}),
                     //scrollsTypes["CardListPopup"].Methods.GetMethod("RenderCost", new Type[]{typeof(Rect),typeof(Card)}),
                     scrollsTypes["CardListPopup"].Methods.GetMethod("OnGUI")[0],
                     scrollsTypes["CardListPopup"].Methods.GetMethod("SetButtonHighlighted", new Type[]{typeof(ECardListButton),typeof(bool)}),
                     scrollsTypes["CardListPopup"].Methods.GetMethod("SetButtonContent", new Type[]{typeof(ECardListButton),typeof(GUIContent)}),
                      scrollsTypes["CardListPopup"].Methods.GetMethod("SetButtonEnabled", new Type[]{typeof(ECardListButton),typeof(bool)}),
                      scrollsTypes["CardListPopup"].Methods.GetMethod("SetOffX", new Type[]{typeof(float)}),
                      scrollsTypes["CardListPopup"].Methods.GetMethod("SetOpacity", new Type[]{typeof(float)}),
                      scrollsTypes["TradeSystem"].Methods.GetMethod("Init", new Type[]{typeof(float),typeof(float),typeof(float),typeof(float),typeof(RenderTexture)}),


             };
            }
            catch
            {
                return new MethodDefinition[] { };
            }
		}

        public override bool BeforeInvoke(InvocationInfo info, out object returnValue) {

            returnValue = null;
            if (info.target is CardListPopup && info.targetMethod.Equals("OnGUI"))
            {

                classdata dis = cardlists[(CardListPopup)info.target];
                if(dis.offX<=(float)Screen.width * -0.5f){return true;} //in store if sell menu issnt shown, dont draw it!

                
                GUI.depth = 15;
                GUI.skin = dis.cardListPopupSkin;
                GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, dis.opacity);
                Rect position = new Rect(dis.outerRect.x + dis.offX, dis.outerRect.y, dis.outerRect.width, dis.outerRect.height);
                Rect position2 = new Rect(dis.innerBGRect.x + dis.offX, dis.innerBGRect.y, dis.innerBGRect.width, dis.innerBGRect.height);
                Rect position3 = new Rect(dis.innerRect.x + dis.offX, dis.innerRect.y, dis.innerRect.width, dis.innerRect.height);
                Rect position4 = new Rect(dis.buttonLeftRect.x + dis.offX, dis.buttonLeftRect.y, dis.buttonLeftRect.width, dis.buttonLeftRect.height);
                Rect position5 = new Rect(dis.buttonRightRect.x + dis.offX, dis.buttonRightRect.y, dis.buttonRightRect.width, dis.buttonRightRect.height);
                if (dis.showFrame)
                {
                    GUI.Box(position, string.Empty);
                }
                GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, dis.opacity * 0.3f);
                GUI.Box(position2, string.Empty);
                GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, dis.opacity);
                dis.cardListPopupBigLabelSkin.label.fontSize = (int)(dis.fieldHeight / 1.7f);
                dis.cardListPopupSkin.label.fontSize = (int)(dis.fieldHeight / 2.5f);
                dis.scrollPos = GUI.BeginScrollView(position3, dis.scrollPos, new Rect(0f, 0f, dis.innerRect.width - 20f, dis.fieldHeight * (float)dis.cards.Count));
                int num = 0;
                Card card = null;

                foreach (Card current in dis.cards)
                {
                    if (!current.tradable)
                    {
                        GUI.color = new Color(1f, 1f, 1f, 0.5f);
                    }
                    GUI.skin = dis.cardListPopupGradientSkin;
                    Rect position6 = new Rect(dis.fieldHeight + 2f, (float)num * dis.fieldHeight, dis.innerRect.width - dis.scrollBarSize - dis.fieldHeight - 2f, dis.fieldHeight);
                    if (position6.yMax < dis.scrollPos.y || position6.y > dis.scrollPos.y + position3.height)
                    {
                        num++;
                        GUI.color = Color.white;
                    }
                    else
                    {
                        if (dis.clickableItems)
                        {
                            if (GUI.Button(position6, string.Empty))
                            {
                                dis.callback.ItemClicked((CardListPopup)info.target, current);
                            }
                        }
                        else
                        {
                            GUI.Box(position6, string.Empty);
                        }
                        //Texture texture = App.AssetLoader.LoadTexture2D(string.Empty + current.getCardImage());
                        if (!cardtextures.ContainsKey(current.getCardImage()))
                        {
                            Texture txtre = App.AssetLoader.LoadTexture2D(string.Empty + current.getCardImage());
                            cardtextures.Add(current.getCardImage(), txtre);
                        }
                        Texture texture = cardtextures[current.getCardImage()];
                        if (texture != null)
                        {
                            GUI.DrawTexture(new Rect(dis.fieldHeight + dis.fieldHeight * 0.21f, (float)num * dis.fieldHeight + (dis.fieldHeight - dis.cardHeight) * 0.43f, dis.cardWidth, dis.cardHeight), texture);
                        }
                        GUI.skin = dis.cardListPopupBigLabelSkin;
                        string name = current.getName();
                        Vector2 vector = GUI.skin.label.CalcSize(new GUIContent(name));
                        Rect position7 = new Rect(dis.labelX, (float)num * dis.fieldHeight - 3f + dis.fieldHeight * 0.01f, dis.labelsWidth, dis.cardHeight);
                        GUI.Label(position7, (vector.x >= position7.width) ? (name.Substring(0, Mathf.Min(name.Length, dis.maxCharsName)) + "...") : name);
                        GUI.skin = dis.cardListPopupSkin;
                        string text = current.getPieceKind().ToString();
                        string str = text.Substring(0, 1) + text.Substring(1).ToLower();
                        string text2 = current.getRarityString() + ", " + str;
                        Vector2 vector2 = GUI.skin.label.CalcSize(new GUIContent(text2));
                        Rect position8 = new Rect(dis.labelX, (float)num * dis.fieldHeight - 3f + dis.fieldHeight * 0.57f, dis.labelsWidth, dis.cardHeight);
                        GUI.Label(position8, (vector2.x >= position8.width) ? (text2.Substring(0, Mathf.Min(text2.Length, dis.maxCharsRK)) + "...") : text2);
                        this.Rendercostss(new Rect(dis.labelX + dis.labelsWidth + (dis.costIconSize - dis.costIconWidth) / 2f - 5f, (float)num * dis.fieldHeight + (dis.fieldHeight - dis.costIconHeight) / 2f, dis.costIconWidth, dis.costIconHeight), current);
                        GUI.skin = dis.cardListPopupLeftButtonSkin;
                        Rect position9 = new Rect(0f, (float)num * dis.fieldHeight, dis.fieldHeight, dis.fieldHeight);
                        if (dis.itemButtonTexture == null && !dis.selectable)
                        {
                            GUI.enabled = false;
                        }
                        if (GUI.Button(position9, string.Empty) && current.tradable)
                        {
                            if (dis.selectable)
                            {
                                if (!dis.selectedCards.Contains(current))
                                {
                                    dis.selectedCards.Add(current);
                                }
                                else
                                {
                                    dis.selectedCards.Remove(current);
                                }
                            }
                            else
                            {
                                card = current;
                            }
                            App.AudioScript.PlaySFX("Sounds/hyperduck/UI/ui_button_click");
                        }
                        if (dis.itemButtonTexture == null && !dis.selectable)
                        {
                            GUI.enabled = true;
                        }
                        if (current.tradable)
                        {
                            if (dis.selectable)
                            {
                                if (dis.selectedCards.Contains(current))
                                {
                                    GUI.DrawTexture(position9, button_cb_checked);
                                }
                                else
                                {
                                    GUI.DrawTexture(position9, button_cb);
                                }
                            }
                            else
                            {
                                if (dis.itemButtonTexture != null)
                                {
                                    GUI.DrawTexture(position9, dis.itemButtonTexture);
                                }
                            }
                        }
                        if (!current.tradable)
                        {
                            GUI.color = Color.white;
                        }
                        num++;
                    }
                } // ende for current card in cardlist
                GUI.EndScrollView();
                if (card != null)
                {
                    dis.callback.ItemButtonClicked((CardListPopup)info.target, card);
                }
                GUI.skin = dis.lobbySkin;
                if (dis.buttonLeftContent != null)
                {
                    if (!dis.leftButtonEnabled)
                    {
                        GUI.enabled = false;
                    }
                    if (GUI.Button(position4, dis.buttonLeftContent))
                    {
                        if (dis.selectable)
                        {
                            dis.callback.ButtonClicked((CardListPopup)info.target, ECardListButton.BUTTON_LEFT, new List<Card>(dis.selectedCards));
                            dis.selectedCards.Clear();
                        }
                        else
                        {
                            dis.callback.ButtonClicked((CardListPopup)info.target, ECardListButton.BUTTON_LEFT);
                        }
                        App.AudioScript.PlaySFX("Sounds/hyperduck/UI/ui_button_click");
                    }
                    Rect position10 = new Rect(position4.x + position4.height * 0.01f, position4.y, position4.height, position4.height);
                    if (dis.leftButtonHighlighted)
                    {
                        GUI.DrawTexture(position10, button_cb_checked);
                    }
                    else
                    {
                        if (dis.leftHighlightable)
                        {
                            GUI.DrawTexture(position10, button_cb);
                        }
                    }
                    GUI.Label(position4, dis.buttonLeftContent);
                    if (!dis.leftButtonEnabled)
                    {
                        GUI.enabled = true;
                    }
                }
                if (dis.buttonRightContent != null)
                {
                    if (!dis.rightButtonEnabled)
                    {
                        GUI.enabled = false;
                    }
                    if (GUI.Button(position5, dis.buttonRightContent))
                    {
                        if (dis.selectable)
                        {
                            dis.callback.ButtonClicked((CardListPopup)info.target, ECardListButton.BUTTON_RIGHT, new List<Card>(dis.selectedCards));
                            dis.selectedCards.Clear();
                        }
                        else
                        {
                            dis.callback.ButtonClicked((CardListPopup)info.target, ECardListButton.BUTTON_RIGHT);
                        }
                        App.AudioScript.PlaySFX("Sounds/hyperduck/UI/ui_button_click");
                    }
                    Rect position11 = new Rect(position5.x + position5.height * 0.01f, position5.y, position5.height, position5.height);
                    if (dis.rightButtonHighlighted)
                    {
                        GUI.DrawTexture(position11, button_cb_checked);
                    }
                    else
                    {
                        if (dis.rightHighlightable)
                        {
                            GUI.DrawTexture(position11, button_cb);
                        }
                    }
                    GUI.Label(position5, dis.buttonRightContent);
                    if (!dis.rightButtonEnabled)
                    {
                        GUI.enabled = false;
                    }
                }






                return true;
            }


            
            return false;
        
        }

        public override void AfterInvoke (InvocationInfo info, ref object returnValue)
        //public override bool BeforeInvoke(InvocationInfo info, out object returnValue)
        {
            //start
            if (info.target is CardListPopup && info.targetMethod.Equals("Start"))
            {

                if (!cardlists.ContainsKey((CardListPopup)info.target))
                {
                    cardlists.Add((CardListPopup)info.target, new classdata());
                }
                classdata dis = cardlists[(CardListPopup)info.target];

                dis.lobbySkin = (GUISkin)Resources.Load("_GUISkins/Lobby");
                dis.cardListPopupSkin = (GUISkin)Resources.Load("_GUISkins/CardListPopup");
                dis.cardListPopupGradientSkin = (GUISkin)Resources.Load("_GUISkins/CardListPopupGradient");
                dis.cardListPopupBigLabelSkin = (GUISkin)Resources.Load("_GUISkins/CardListPopupBigLabel");
                dis.cardListPopupLeftButtonSkin = (GUISkin)Resources.Load("_GUISkins/CardListPopupLeftButton");

            }
            //init 
            if (info.target is CardListPopup && info.targetMethod.Equals("Init"))
            {

                if (!cardlists.ContainsKey((CardListPopup)info.target))
                {
                    cardlists.Add((CardListPopup)info.target, new classdata());
                }
               
                Rect screenRect =(Rect)info.arguments[0];
                bool showFrame = (bool)info.arguments[1];
                bool selectable = (bool)info.arguments[2];
                List<Card> cards = (List<Card>)info.arguments[3];
                foreach (Card current in cards) 
                {
                    if (!cardtextures.ContainsKey(current.getCardImage())) 
                    { 
                        Texture texture = App.AssetLoader.LoadTexture2D(string.Empty + current.getCardImage());
                        cardtextures.Add(current.getCardImage(), texture);
                    
                    
                    }
                
                
                
                }
                ICardListCallback callback = (ICardListCallback)info.arguments[4];
                GUIContent buttonLeftContent = (GUIContent)info.arguments[5];
                GUIContent buttonRightContent = (GUIContent)info.arguments[6];
                bool leftButtonEnabled = (bool)info.arguments[7];
                bool rightButtonEnabled = (bool)info.arguments[8];
                bool leftHighlightable = (bool)info.arguments[9];
                bool rightHighlightable = (bool)info.arguments[10];
                Texture itemButtonTexture = (Texture)info.arguments[11];
                bool clickableItems = (bool)info.arguments[12];

                classdata dis = cardlists[(CardListPopup)info.target];

                dis.showFrame = showFrame;
                dis.selectable = selectable;
                dis.cards = cards;
                dis.callback = callback;
                dis.buttonLeftContent = buttonLeftContent;
                dis.buttonRightContent = buttonRightContent;
                dis.leftButtonEnabled = leftButtonEnabled;
                dis.rightButtonEnabled = rightButtonEnabled;
                dis.itemButtonTexture = itemButtonTexture;
                dis.leftHighlightable = leftHighlightable;
                dis.rightHighlightable = rightHighlightable;
                dis.clickableItems = clickableItems;
                if (showFrame)
                {
                    dis.margins = new Vector4(12f, 12f, 12f, 12f + dis.BOTTOM_MARGIN_EXTRA);
                }
                else
                {
                    dis.margins = new Vector4(0f, 0f, 0f, 0f + dis.BOTTOM_MARGIN_EXTRA);
                }
                dis.outerRect = screenRect;
                dis.innerBGRect = new Rect(dis.outerRect.x + dis.margins.x, dis.outerRect.y + dis.margins.y, dis.outerRect.width - (dis.margins.x + dis.margins.z), dis.outerRect.height - (dis.margins.y + dis.margins.w));
                float num = 0.005f * (float)Screen.width;
                dis.innerRect = new Rect(dis.innerBGRect.x + num, dis.innerBGRect.y + num, dis.innerBGRect.width - 2f * num, dis.innerBGRect.height - 2f * num);
                float num2 = dis.BOTTOM_MARGIN_EXTRA - 0.01f * (float)Screen.height;
                dis.buttonLeftRect = new Rect(dis.innerRect.x + dis.innerRect.width * 0.03f, dis.innerBGRect.yMax + num2 * 0.28f, dis.innerRect.width * 0.45f, num2);
                dis.buttonRightRect = new Rect(dis.innerRect.xMax - dis.innerRect.width * 0.48f, dis.innerBGRect.yMax + num2 * 0.28f, dis.innerRect.width * 0.45f, num2);
                float num3 = (float)Screen.height / (float)Screen.width * 0.25f;
                dis.fieldHeight = (dis.innerRect.width - dis.scrollBarSize) / (1f / num3 + 1f);
                dis.costIconSize = dis.fieldHeight;
                dis.costIconWidth = dis.fieldHeight / 2f;
                dis.costIconHeight = dis.costIconWidth * 72f / 73f;
                dis.cardHeight = dis.fieldHeight * 0.72f;
                dis.cardWidth = dis.cardHeight * 100f / 75f;
                dis.labelX = dis.fieldHeight + dis.cardWidth * 1.45f;
                dis.labelsWidth = dis.innerRect.width - dis.labelX - dis.costIconSize - dis.scrollBarSize;
                dis.maxCharsName = (int)(dis.labelsWidth / 12f);
                dis.maxCharsRK = (int)(dis.labelsWidth / 10f);


            }
            //cardlist
            if (info.target is CardListPopup && info.targetMethod.Equals("SetCardList"))
            {

                classdata dis = cardlists[(CardListPopup)info.target];
                dis.cards = (List<Card>)info.arguments[0];
                foreach (Card current in dis.cards)
                {
                    if (!cardtextures.ContainsKey(current.getCardImage()))
                    {
                        Texture texture = App.AssetLoader.LoadTexture2D(string.Empty + current.getCardImage());
                        cardtextures.Add(current.getCardImage(), texture);
                    }
                }

            }
            //update
            if (info.target is CardListPopup && info.targetMethod.Equals("Update"))
            {

                if (!cardlists.ContainsKey((CardListPopup)info.target))
                {
                    cardlists.Add((CardListPopup)info.target, new classdata());
                }

                classdata dis = cardlists[(CardListPopup)info.target];
                Vector3 vector = new Vector3(Input.mousePosition.x, (float)Screen.height - Input.mousePosition.y);
                bool flag = dis.innerRect.Contains(vector);
                bool flag2 = false;
                int num = 0;
                foreach (Card current in dis.cards)
                {
                    Rect rect = new Rect(0f, (float)num * dis.fieldHeight, dis.innerRect.width - dis.scrollBarSize, dis.fieldHeight);
                    if (flag && rect.Contains(vector - new Vector3(dis.innerRect.x - dis.scrollPos.x, dis.innerRect.y - dis.scrollPos.y)))
                    {
                        flag2 = true;
                        dis.callback.ItemHovered((CardListPopup)info.target, current);
                        break;
                    }
                    num++;
                }
                if (!flag2)
                {
                    dis.callback.ItemHovered((CardListPopup)info.target, null);
                }
            }
            //SetButtonHighlighted
            if (info.target is CardListPopup && info.targetMethod.Equals("SetButtonHighlighted"))
            {

                if (!cardlists.ContainsKey((CardListPopup)info.target))
                {
                    cardlists.Add((CardListPopup)info.target, new classdata());
                }
                classdata dis = cardlists[(CardListPopup)info.target];
                ECardListButton button = (ECardListButton)info.arguments[0];
                bool highlighted = (bool)info.arguments[1];
                if (button != ECardListButton.BUTTON_LEFT)
                {
                    if (button == ECardListButton.BUTTON_RIGHT)
                    {
                        dis.rightButtonHighlighted = highlighted;
                    }
                }
                else
                {
                    dis.leftButtonHighlighted = highlighted;
                }

            }

            //SetButtonContent
            if (info.target is CardListPopup && info.targetMethod.Equals("SetButtonContent"))
            {

                if (!cardlists.ContainsKey((CardListPopup)info.target))
                {
                    cardlists.Add((CardListPopup)info.target, new classdata());
                }
                classdata dis = cardlists[(CardListPopup)info.target];
                ECardListButton button = (ECardListButton)info.arguments[0];
                GUIContent content = (GUIContent)info.arguments[1];
                if (button != ECardListButton.BUTTON_LEFT)
                {
                    if (button == ECardListButton.BUTTON_RIGHT)
                    {
                        dis.buttonRightContent = content;
                    }
                }
                else
                {
                    dis.buttonLeftContent = content;
                }

            }

            //SetButtonEnabled
            if (info.target is CardListPopup && info.targetMethod.Equals("SetButtonHighlighted"))
            {

                if (!cardlists.ContainsKey((CardListPopup)info.target))
                {
                    cardlists.Add((CardListPopup)info.target, new classdata());
                }
                classdata dis = cardlists[(CardListPopup)info.target];
                ECardListButton button = (ECardListButton)info.arguments[0];
                bool enabled = (bool)info.arguments[1];
                if (button != ECardListButton.BUTTON_LEFT)
                {
                    if (button == ECardListButton.BUTTON_RIGHT)
                    {
                        dis.rightButtonEnabled = enabled;
                    }
                }
                else
                {
                    dis.leftButtonEnabled = enabled;
                }

            }
            //offX
            if (info.target is CardListPopup && info.targetMethod.Equals("SetOffX"))
            {
                classdata dis;
                if (!cardlists.ContainsKey((CardListPopup)info.target))
                {
                    cardlists.Add((CardListPopup)info.target, new classdata());
                    dis = cardlists[(CardListPopup)info.target];
                    dis.lobbySkin = (GUISkin)Resources.Load("_GUISkins/Lobby");
                    dis.cardListPopupSkin = (GUISkin)Resources.Load("_GUISkins/CardListPopup");
                    dis.cardListPopupGradientSkin = (GUISkin)Resources.Load("_GUISkins/CardListPopupGradient");
                    dis.cardListPopupBigLabelSkin = (GUISkin)Resources.Load("_GUISkins/CardListPopupBigLabel");
                    dis.cardListPopupLeftButtonSkin = (GUISkin)Resources.Load("_GUISkins/CardListPopupLeftButton");
                }
                dis = cardlists[(CardListPopup)info.target];
               
                
                dis.offX = (float)info.arguments[0];

            }
            //SetOpacity
            if (info.target is CardListPopup && info.targetMethod.Equals("SetOpacity"))
            {
                
                if (!cardlists.ContainsKey((CardListPopup)info.target))
                {
                    cardlists.Add((CardListPopup)info.target, new classdata());

                }
                classdata dis = cardlists[(CardListPopup)info.target];

                dis.opacity = (float)info.arguments[0];

            }

            //TradeSysteminit
            if (info.target is TradeSystem && info.targetMethod.Equals("Init"))
            { // lobby/tadesystem wont call start() or init() Y_Y bad, bad lobby/tradesystem
                CardListPopup clInventoryP1 = (CardListPopup)typeof(TradeSystem).GetField("clInventoryP1", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(info.target);
                if (!cardlists.ContainsKey(clInventoryP1))
                {
                    cardlists.Add(clInventoryP1, new classdata());

                }
                CardListPopup clOfferP1 = (CardListPopup)typeof(TradeSystem).GetField("clOfferP1", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(info.target);
                if (!cardlists.ContainsKey(clOfferP1))
                {
                    cardlists.Add(clOfferP1, new classdata());

                }
                CardListPopup clInventoryP2 = (CardListPopup)typeof(TradeSystem).GetField("clInventoryP2", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(info.target);
                if (!cardlists.ContainsKey(clInventoryP2))
                {
                    cardlists.Add(clInventoryP2, new classdata());

                }
                CardListPopup clOfferP2 = (CardListPopup)typeof(TradeSystem).GetField("clOfferP2", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(info.target);
                if (!cardlists.ContainsKey(clOfferP2))
                {
                    cardlists.Add(clOfferP2, new classdata());

                }
                //add alot of stuff
                addall(clInventoryP1);
                addall(clInventoryP2);
                addall(clOfferP1);
                addall(clOfferP2);


            }

            


            return;
        }




        
	}
}

