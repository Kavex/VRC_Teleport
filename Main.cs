using System;
using System.Collections;
using MelonLoader;
using UnityEngine;
using UnityEngine.UI;

namespace TP
{

    public class Main : MelonMod
    {
        GameObject quickMenu;
        GameObject quickInterfaceMenu;
        public static Transform playlistButton =>
        GameObject.Find("UserInterface/MenuContent/Screens/UserInfo/Buttons/RightSideButtons/RightUpperButtonColumn/PlaylistsButton").transform;

        public override void OnApplicationStart()
        {
            MelonCoroutines.Start(QMInitializer());
        }

        private IEnumerator QMInitializer()
        {
            while ((quickMenu = GameObject.Find("UserInterface/Canvas_QuickMenu(Clone)")) == null)
                yield return null;
            while ((quickInterfaceMenu = GameObject.Find("UserInterface/MenuContent/")) == null)
                yield return null;
            SelMenu();
        }
        public static Button CreateUserInfoButton(string buttonText, System.Action buttonAction)
        {
            Button button = GameObject.Instantiate(playlistButton, playlistButton.parent, true).GetComponent<Button>();
            Text btnText = button.GetComponentInChildren<Text>();

            btnText.text = buttonText;
            button.onClick = new Button.ButtonClickedEvent();
            button.onClick.AddListener(buttonAction);

            return button;
        }

        private void SelMenu()
        {
            Transform quickTP = quickMenu.transform.Find("Container/Window/QMParent/Menu_SelectedUser_Local/ScrollRect/Viewport/VerticalLayoutGroup/Buttons_UserActions");

            Utils.CreateDefaultButton("Teleport", new Vector3(0, -25, 0), "Teleport to selected player", Color.white, quickTP,
                new Action(() => {
                    Utils.LocalPlayer.gameObject.transform.position = Utils.MenuControl.activePlayer.gameObject.transform.position;
                }));

            Button tpButton = CreateUserInfoButton("Teleport", () =>
            {
                Utils.LocalPlayer.gameObject.transform.position = Utils.MenuControl.activePlayer.gameObject.transform.position;
            });
        }
    }
}