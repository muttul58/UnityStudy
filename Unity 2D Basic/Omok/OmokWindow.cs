using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class OmokWindow : EditorWindow
{
	const int size = 14;
	int[,] Cell; // 1 블랙, 2 화이트
	Texture[] CellTextures;
	bool blackTurn = true, isEnd = false;


	[MenuItem("Examples/Omok")]
	static void Init()
	{
		OmokWindow window = GetWindow<OmokWindow>();
		window.minSize = new Vector2(1000, 950);
		window.GameInit();
	}


	void GameInit() 
	{
		Cell = new int[size, size];
		CellTextures = new Texture[2] { Resources.Load<Texture>("R_Black"), Resources.Load<Texture>("R_White") };
	}


	void OnGUI()
	{
		if (isEnd)
		{
			GUI.enabled = false;
			EditorGUILayout.LabelField(blackTurn ? "블랙이 이겼습니다" : "화이트가 이겼습니다");
		}
		else
		{
			EditorGUILayout.LabelField(blackTurn ? "블랙 턴입니다" : "화이트 턴입니다");
		}


		for (int i = 0; i < size; i++)
		{
			GUILayout.BeginHorizontal();
			for (int j = 0; j < size; j++)
			{
				if (GUILayout.Button(GetTexture(i, j), GUILayout.Width(60), GUILayout.Height(60)) && Cell[i, j] == 0)
				{
					Cell[i, j] = blackTurn ? 1 : 2;

					int fiveCheck = FiveCheck(Cell[i, j]);

					if (fiveCheck == 1)
					{
						isEnd = true;
						return;
					}

					else if (fiveCheck == 2 || ThreeThreeCheck(Cell[i, j], i, j) >= 2)
					{
						Cell[i, j] = 0;
						return;
					}

					blackTurn = !blackTurn;
				}
			}
			GUILayout.EndHorizontal();
		}
	}


	Texture GetTexture(int i, int j)
	{
		if (Cell[i, j] == 1) return CellTextures[0];
		else if (Cell[i, j] == 2) return CellTextures[1];
		return null;
	}


	bool InRange(params int[] v)
	{
		for (int i = 0; i < v.Length; i++)
			if (!(v[i] >= 0 && v[i] < size)) return false;

		return true;
	}


	int FiveCheck(int now)
	{
		// 오목아님 0, 오목 1, 육목이상 2
		for (int i = 0; i < size; i++)
		{
			for (int j = 0; j < size; j++)
			{
				if (Cell[i, j] != now) continue;

				//→
				if (InRange(j + 4) && Cell[i, j + 1] == now && Cell[i, j + 2] == now && Cell[i, j + 3] == now && Cell[i, j + 4] == now)
					return (InRange(j + 5) && Cell[i, j + 5] == now) ? 2 : 1;

				//↓
				else if (InRange(i + 4) && Cell[i + 1, j] == now && Cell[i + 2, j] == now && Cell[i + 3, j] == now && Cell[i + 4, j] == now)
					return (InRange(i + 5) && Cell[i + 5, j] == now) ? 2 : 1;

				//↘
				else if (InRange(i + 4, j + 4) && Cell[i + 1, j + 1] == now && Cell[i + 2, j + 2] == now && Cell[i + 3, j + 3] == now && Cell[i + 4, j + 4] == now)
					return (InRange(i + 5, j + 5) && Cell[i + 5, j + 5] == now) ? 2 : 1;

				//↙
				else if (InRange(i + 4, j - 4) && Cell[i + 1, j - 1] == now && Cell[i + 2, j - 2] == now && Cell[i + 3, j - 3] == now && Cell[i + 4, j - 4] == now)
					return (InRange(i + 5, j - 5) && Cell[i + 5, j - 5] == now) ? 2 : 1;
			}
		}
		return 0;
	}


	int ThreeThreeCheck(int now, int i, int j)
	{
		int ThreeValue = 0;

		//→
		if (ThreeCheck(now, i, j - 4, i, j - 3, i, j - 2, i, j - 1, i, j + 1, i, j + 2, i, j + 3, i, j + 4)) ++ThreeValue;

		//↓
		if (ThreeCheck(now, i - 4, j, i - 3, j, i - 2, j, i - 1, j, i + 1, j, i + 2, j, i + 3, j, i + 4, j)) ++ThreeValue;

		//↘
		if (ThreeCheck(now, i - 4, j - 4, i - 3, j - 3, i - 2, j - 2, i - 1, j - 1, i + 1, j + 1, i + 2, j + 2, i + 3, j + 3, i + 4, j + 4)) ++ThreeValue;

		//↙
		if (ThreeCheck(now, i + 4, j - 4, i + 3, j - 3, i + 2, j - 2, i + 1, j - 1, i - 1, j + 1, i - 2, j + 2, i - 3, j + 3, i - 4, j + 4)) ++ThreeValue;


		return ThreeValue;
	}


	bool ThreeCheck(int now, int im4, int jm4, int im3, int jm3, int im2, int jm2, int im1, int jm1, int ip1, int jp1, int ip2, int jp2, int ip3, int jp3, int ip4, int jp4)
	{
		if (InRange(im4, jm4, ip1, jp1))
			if (Cell[im4, jm4] == 0 && Cell[im3, jm3] == now && Cell[ip1, jp1] == 0)
			{
				if (Cell[im2, jm2] == now && Cell[im1, jm1] == 0) return true;
				if (Cell[im2, jm2] == 0 && Cell[im1, jm1] == now) return true;
			}

		if (InRange(im3, jm3, ip1, jp1))
			if (Cell[im3, jm3] == 0 && Cell[im2, jm2] == now && Cell[im1, jm1] == now && Cell[ip1, jp1] == 0) return true;

		if (InRange(im3, jm3, ip2, jp2))
			if (Cell[im3, jm3] == 0 && Cell[im2, jm2] == now && Cell[im1, jm1] == 0 && Cell[ip1, jp1] == now && Cell[ip2, jp2] == 0) return true;

		if (InRange(im2, jm2, ip2, jp2)) // 중앙
			if (Cell[im2, jm2] == 0 && Cell[im1, jm1] == now && Cell[ip1, jp1] == now && Cell[ip2, jp2] == 0) return true;

		if (InRange(im2, jm2, ip3, jp3))
			if (Cell[im2, jm2] == 0 && Cell[im1, jm1] == now && Cell[ip1, jp1] == 0 && Cell[ip2, jp2] == now && Cell[ip3, jp3] == 0) return true;

		if (InRange(im1, jm1, ip3, jp3))
			if (Cell[im1, jm1] == 0 && Cell[ip1, jp1] == now && Cell[ip2, jp2] == now && Cell[ip3, jp3] == 0) return true;

		if (InRange(im1, jm1, ip4, jp4))
			if (Cell[im1, jm1] == 0 && Cell[ip3, jp3] == now && Cell[ip4, jp4] == 0)
			{
				if (Cell[ip1, jp1] == now && Cell[ip2, jp2] == 0) return true;
				if (Cell[ip1, jp1] == 0 && Cell[ip2, jp2] == now) return true;
			}

		return false;
	}
}
