#pragma once
#include "Constants.h"

namespace Contra
{
	struct Game;

	struct Map
	{
		sf::Sprite backgroundSprite;
		sf::Texture backgroundTexture;

		sf::Texture groundTexture;
		sf::Sprite groundSprite;
	};

	void InitBackground(Map& background);
	void InitGround(Map& ground, sf::RenderWindow& window);
}