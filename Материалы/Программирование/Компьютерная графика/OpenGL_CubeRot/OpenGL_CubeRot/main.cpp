#include <glut.h> //подключаем заголовочный файл glut.h


// Переменные хранящие инфромацию о углах поворота и угле, изменяющий скорость вращения
GLfloat angleX = 0.0f, angleY = 0.0f, angleZ = 0.0f, deltaAngle = 0.0f;



void renderScene(void) {

	
	// очистка буфера цвета и глубины
	glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);
	// обнуление трансформации
	glLoadIdentity();
	// установка позиции камеры 
	gluLookAt(10.0f, 10.0f, 10.0f,
				0.0f, 0.0f, 0.0f,
				0.0f, 1.0f, 0.0f);


	glColor3f(1.0f, 0.5f, 0.25f);


	glRotatef(angleX, 1, 0, 0);
	glRotatef(angleY, 0, 1, 0);
	glRotatef(angleZ, 0, 0, 1);
	glutSolidCube(5.0f);

	
	
	//Если не стандартная фигура, то рисовать таким образом
/*glBegin(GL_TRIANGLES);
	glVertex3f(-2.0f, -2.0f, 0.0f);
	glVertex3f(0.0f, 2.0f, 0.0);
	glVertex3f(2.0f, -2.0f, 0.0);
	glEnd();*/

	
	

	glutSwapBuffers();
}

//функция вызывается, когда изменяется размер окна, чтоб сохранить пропорции объектов
void changeSize(int w, int h) {

	// предупредим деление на ноль
	// если окно сильно перетянуто будет
	if (h == 0)
		h = 1;
	float ratio = 1.0* w / h;

	// используем матрицу проекции
	glMatrixMode(GL_PROJECTION);

	// Сбрасываем матрицу к единичной
	glLoadIdentity();

	// определяем окно просмотра
	glViewport(0, 0, w, h);

	// установить корректную перспективу.
	gluPerspective(45, ratio, 1, 1000);

	// вернуться к модели
	glMatrixMode(GL_MODELVIEW);
}

//обработка нажатий клавиш стрелок, для смены направления движения
void processSpecialKeys(int key, int x, int y) {
	switch (key) 
	{
		case GLUT_KEY_DOWN:
			angleX += (10.0f + deltaAngle);
			break;
		case GLUT_KEY_UP:
			angleX -= (10.0f + deltaAngle);
			break;
		case GLUT_KEY_LEFT:
			angleY -= (10.0f + deltaAngle);
			break;
		case GLUT_KEY_RIGHT:
			angleY += (10.0f + deltaAngle);
			break;
	}
}

//обработка нажатий клавиш + и -, для смены увеличения и уменьшения скорости вращения
void processNormalKeys(unsigned char key, int x, int y) {
	switch (key)
	{
	case '+': deltaAngle += 5.0f;
		break;
	case '-': deltaAngle -= 5.0f;
		break;
	}
}

int main(int argc, char **argv) {

	// Инициализация GLUT
	glutInit(&argc, argv);
	glutInitDisplayMode(GLUT_DEPTH | GLUT_DOUBLE | GLUT_RGBA);
	glutInitWindowPosition(100, 100);
	glutInitWindowSize(700, 500);
	glutCreateWindow("Вращение куба");
	// регистрация обратных вызовов
	glutDisplayFunc(renderScene);

	// регистрация изменения размеров окна
	glutReshapeFunc(changeSize);

	// регистрация функции, которая вызывается, когда приложение находится в режиме ожидания
	glutIdleFunc(renderScene);

	// регистрация функции, обрабатывающая спец. клавиши
	glutSpecialFunc(processSpecialKeys);
	glutKeyboardFunc(processNormalKeys);

	// Включение ряда параметров OpenGL
	glEnable(GL_ALPHA_TEST); // Учет прозрачности
	glEnable(GL_DEPTH_TEST); // Удаление невидимых поверхностей
	glEnable(GL_COLOR_MATERIAL); // Синхронное задание цвета рисования и цвета материала объектов
	glEnable(GL_LIGHTING); // Разрешение использование источников света
	glBlendFunc(GL_SRC_ALPHA, GL_ONE_MINUS_SRC_ALPHA);// Разрешение смешения цветов
	glEnable(GL_BLEND); // Разрешение смешения цветов
	glEnable(GL_LIGHT0); // Включение нулевого источника света
	// Задание положения и направления нулевого источника света
	float pos[4] = { 3, 3, 3, 1 };
	float dir[3] = { -1, -1, -1 };
	glLightfv(GL_LIGHT0, GL_POSITION, pos);
	glLightfv(GL_LIGHT0, GL_SPOT_DIRECTION, dir);

	// Основной цикл GLUT
	glutMainLoop();

	return 1;
}