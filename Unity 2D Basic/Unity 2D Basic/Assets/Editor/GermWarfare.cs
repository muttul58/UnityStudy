using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class GermWarfare : EditorWindow //OmokWindow
{
	const int size = 7; // 맵 크기 : 7칸 X 7칸
	int[,] Cell; // Cell 이름의 2차원 배열 선원  /  1 블루, 2 레드
	//Assets > Resources 에 V_Blue_0.png, V_Red_0.png 추가하면 초기화 됨
	Texture[] CellTextures;	// 세균 이미지 저장용 배열 선언
	bool BlueTurn = true, isEnd = false; // V_Blue_0 : 처음 턴 블루부터    /  isEnd : 끝 확인
    int BlueCount = 0, RedCount = 0, BlankCount = 0;
	string str="";  // Blue, Red 갯수 출력용
	int SelectCount = 0; // 선택 횟수 0:선택없음, 1:처음선택(블루 또는 래드 선택), 2:두번째선택(이동할 위치 선택)
	int PreviousLocation_R = 0, PreviousLocation_C = 0;
	int JumpBlankCnt_B=1, JumpBlankCnt_R=1;


	// Unity 메뉴에 'muttul' 라는 메뉴가 추가됨, muttul 하위 메뉴로 '세균전' 생성
	[MenuItem("muttul/세균전")]
	static void Init()
	{
		//'GermWarfare' 이름으로 윈도 창 생성
		GermWarfare window = GetWindow<GermWarfare>();
		window.minSize = new Vector2(445, 500);  // 윈도 창 크기 : 445 x 500
		window.GameInit();  // window.GameInit() 함수 실행
	}


	void GameInit() 
	{
		Cell = new int[size, size]; // 배열 생성 후 0으로 초기화 됨
		Cell[0,0] = 1;  // V_Blue_0
		Cell[6,6] = 1;  // V_Red_0
		Cell[0,6] = 2;  // V_Blue_0
		Cell[6,0] = 2;  // V_Red_0
		
		// CellTextures 형의 Texture 배열 생성 후 V_Blue_0.png와 V_Red_0.png 저장
		CellTextures = new Texture[5] { Resources.Load<Texture>("V_Blue_0"),
		                                Resources.Load<Texture>("V_Red_0"),
										Resources.Load<Texture>("V_Blue_1"),
										Resources.Load<Texture>("V_Red_1"),
										Resources.Load<Texture>("Move") };
	}


	void OnGUI()
	{
		str = "[Blue: "+ BlueCount.ToString()+ "]    [Red: "+ RedCount.ToString()+ "]";
		BlueRedCount();  // 블루, 래드, 공백 갯수 카운터
		EndCheck();  // 종료 확인
		
		if (isEnd) // 게임 종료 확인
		{
			// 게임이 종료 이면서 블루 턴이면 출력
			GUI.enabled = false;  // 더 이상 게임을 할 수 없도록 막음
			EditorGUILayout.LabelField( BlueCount > RedCount ? "블루가 이겼습니다.            " + str :
			                               				       "래드가 이겼습니다.            " + str);
		}
		else
		{
			// 게임이 종료가 아니고 블루 턴이면 출력
			EditorGUILayout.LabelField( BlueTurn ? "블루 턴입니다.            " + str :
			                                       "래드 턴입니다.            " + str);
		}

		EditorGUILayout.LabelField("SelectCount : " + SelectCount.ToString());


		for (int i = 0; i < size; i++)
		{
			// 버튼을 가로 순으로 생성
			GUILayout.BeginHorizontal();
			for (int j = 0; j < size; j++)
			{

				// 버튼을 클릭 했을 때 셀값이 0이면 // 버튼이 눌린적이 없을 때를 뜻함.
				if (GUILayout.Button(GetTexture(i, j), GUILayout.Width(60), GUILayout.Height(60)))
				{
					// 블루의 처음 선택인 경우
					if (SelectCount == 0 && BlueTurn == true && Cell[i, j]==1)
					{
						Cell[i, j] = 3;
						MoveLocationShow(i, j);
						PreviousLocation_R = i;
						PreviousLocation_C = j;
						SelectCount++;
					}
					// 블루의 두번째 선택이 취소인 경우
					else if (SelectCount == 1 && BlueTurn == true && Cell[i,j]==3)
					{
						Cell[i, j] = 1;
						SelectLocationClear();
						SelectCount--;
					}
					// 래드의 처음 선택인 경우
					else if (SelectCount == 0 && BlueTurn == false && Cell[i, j]==2)
					{
						Cell[i, j] = 4;
						MoveLocationShow(i, j);
						PreviousLocation_R = i;
						PreviousLocation_C = j;
						SelectCount++;
					}
					// 래드의 두번째 선택이 취소인 경우
					else if (SelectCount == 1 && BlueTurn == false && Cell[i,j]==4)
					{
						Cell[i, j] = 2;
						SelectLocationClear();
						SelectCount--;
					}

					// 복사  또는 이동 공간 선택한 경우
					if (Cell[i, j] == 5)
					{
						// 이동 공간 선택한 경우
						if (Mathf.Abs(PreviousLocation_R-i) == 2 || Mathf.Abs(PreviousLocation_C - j) == 2)
						{
							Cell[PreviousLocation_R, PreviousLocation_C] = 0;
						}

						// 복사 공간 선택한 경우
						Cell[i, j] = BlueTurn ? 1 : 2;
						SelectCount++;	// 선택 횟수 1증가
						SelectLocationClear();  // 선택 표시 지우기
						Infection(i, j);  // 감염 시키기(블루 -> 래드, 래드 -> 블루 변경)
					}

					// 턴 넘기기
					if (SelectCount == 2)
					{
						BlueTurn = !BlueTurn;
						SelectCount = 0;
					}

				}
			}
			GUILayout.EndHorizontal();
		}


	}

	// 복사 및 이동 가능 공간 표시
	void MoveLocationShow(int i, int j)
	{
		int Start_R = 0, End_R=0, Start_C = 0, End_C = 0;
		Start_R = (i-2 < 0) ? 0 : i-2;
		End_R = (i+2 > 6) ? 6 : i+2;
		Start_C = (j-2 < 0) ? 0 : j-2;
		End_C = (j+2 > 6) ? 6 : j+2;

		for ( int r = Start_R; r <= End_R; r++)
		{
			for ( int c = Start_C; c <= End_C ; c++)
			{
				if ( Cell[r, c] == 0 ) Cell[r, c] = 5;
			}
		}
	}

	// 세균 감염 설정
	void Infection(int i, int j)
	{
		int Start_R = 0, End_R=0, Start_C = 0, End_C = 0;
		Start_R = (i-1 < 0) ? 0 : i-1;
		End_R = (i+1 > 6) ? 6 : i+1;
		Start_C = (j-1 < 0) ? 0 : j-1;
		End_C = (j+1 > 6) ? 6 : j+1;

		for ( int r = Start_R; r <= End_R; r++)
		{
			for ( int c = Start_C; c <= End_C ; c++)
			{
				if (BlueTurn && Cell[r, c] == 2 ) Cell[r, c] = 1;
				else if (!BlueTurn && Cell[r, c] == 1 ) Cell[r, c] = 2;
			}
		}
	}


	// 선택 및 이동 가능 표시 지우기
	void SelectLocationClear()
	{
		for (int r = 0; r < size; r++)
		{
			for (int c = 0; c < size; c++)
			{
				if ( SelectCount == 2 || SelectCount == 1)
				{
					if (Cell[r, c] == 3 || Cell[r, c] == 4)
						Cell[r, c] = BlueTurn ? 1 : 2;
					if (Cell[r, c] == 5) Cell[r, c] = 0; 
				}
			}
		}
	}


	//  종료 확인
	void EndCheck()
	{
		JumpBlankCnt_B=0;
		JumpBlankCnt_R=0;
		// 블루, 래드, 공백 중 하나라도 0개이면 종료
		if (BlueCount == 0 || RedCount == 0 || BlankCount == 0)
		{
			isEnd = true;
			return;
		} 

		for (int All_r = 0; All_r < size; All_r++)
		{
			for (int All_c = 0; All_c < size; All_c++)
			{
				int Start_R = 0, End_R=0, Start_C = 0, End_C = 0;
				Start_R = ( All_r-2 < 0) ? 0 : All_r-2;
				End_R = ( All_r+2 > 6) ? 6 : All_r+2;
				Start_C = (All_c-2 < 0) ? 0 : All_c-2;
				End_C = (All_c+2 > 6) ? 6 : All_c+2;

				if (Cell[All_r, All_c] != 0)
				{
					for ( int Select_r = Start_R; Select_r <= End_R; Select_r++)
					{
						for ( int Select_c = Start_C; Select_c <= End_C; Select_c++)
						{
							if ((Cell[All_r, All_c] == 1 || Cell[All_r, All_c] == 3) && (Cell[Select_r, Select_c] == 0 || Cell[Select_r, Select_c] == 5)) JumpBlankCnt_B++;
							if ((Cell[All_r, All_c] == 2 || Cell[All_r, All_c] == 4) && (Cell[Select_r, Select_c] == 0 || Cell[Select_r, Select_c] == 5)) JumpBlankCnt_R++;
						}
					}
				}
			}
		}
		string s = "[JumpBlankCnt_B: "+ JumpBlankCnt_B.ToString()+ "]    [JumpBlankCnt_R: "+ JumpBlankCnt_R.ToString()+ "]";
		EditorGUILayout.LabelField( BlueTurn ? "블루 턴입니다.            " + s :
										       "래드 턴입니다.            " + s);
		if (JumpBlankCnt_B == 0 || JumpBlankCnt_R == 0 ) isEnd = true;
	}



	//버튼의 이미지 지정하기 Cell[i, j] == 1 명 V_Blue_0.png 아니면 V_Red_0.png 지정
	Texture GetTexture(int i, int j)
	{
		if (Cell[i, j] == 1) return CellTextures[0];
		else if (Cell[i, j] == 2) return CellTextures[1];
		else if (Cell[i, j] == 3) return CellTextures[2];
		else if (Cell[i, j] == 4) return CellTextures[3];
		else if (Cell[i, j] == 5) return CellTextures[4];
		return null;
	}


	// 블루, 래드, 공백 갯수 세기
	void BlueRedCount()
	{
		BlueCount = 0;
		RedCount = 0;
		BlankCount = 0;
		for (int i = 0; i < size; i++)
		{
			for (int j = 0; j < size; j++)
			{
				if (Cell[i, j] == 1 || Cell[i, j] == 3 )  BlueCount++;
				else if(Cell[i, j]  == 2 || Cell[i, j] == 4 ) RedCount++;
				else if(Cell[i, j] == 0 || Cell[i, j] == 5 ) BlankCount++;
			}
		}
	}
}

