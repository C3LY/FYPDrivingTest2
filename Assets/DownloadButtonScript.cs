using System;
using System.Collections;
using System.Collections.Generic;
using Common;
using Microsoft.Win32.SafeHandles;
using UnityEngine;
using UnityEngine.UI;
using Button = UnityEngine.UIElements.Button;

public class DownloadButtonScript : MonoBehaviour
{
   public void TaskOnClick()
   {
      DownloadFileHelper.DownloadToFile(GameManager.Instance.fileText,GameManager.Instance.filename);
   }
}
