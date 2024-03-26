#include "Map.h"
#include "Game.h"

namespace Contra 
{
	void InitBackground(Map& background)
	{
		background.backgroundTexture.loadFromFile(RESOURCES_PATH + "Assets/Hell.png");
		background.backgroundSprite.setTexture(background.backgroundTexture);
	}

	void InitGround(Map& ground, sf::RenderWindow& window)
	{
		const float GROUND_TOP = window.getSize().y - 100.f;
		ground.groundTexture.loadFromFile(RESOURCES_PATH + "Assets/Skull_Ground.png");
		ground.groundSprite.setTexture(ground.groundTexture);
		ground.groundSprite.setPosition(0.f, GROUND_TOP);
		ground.groundSprite.setScale(3.f, 1.f);
	}
}