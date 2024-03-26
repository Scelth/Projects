#pragma once
#include <SFML/Graphics.hpp>
#include <string>
#include <cstdlib>

namespace Contra
{
	const std::string RESOURCES_PATH = "Resources/";
	const int SCREEN_WIDTH = 800;
	const int SCREEN_HEIGTH = 600;
	const float INITIAL_SPEED = 100.f;
	const sf::Vector2f drawScale(3.f, 3.f);
}