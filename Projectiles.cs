using System.Collections.Generic;

namespace AASS
{
    public class Projectiles
    {
        public void Update(Dictionary<string,List<GameObject>> _gameObjects)
        {
            var projectiles = new Dictionary<string,List<GameObject>>();
            foreach(var gameObjects in _gameObjects)
            {
                foreach(var gameObject in gameObjects.Value)
                {
                    if(gameObject is ICreateProjectile createProjectile)
                    {
                        var projectile = createProjectile.CreateProjectile();
                        if(projectile == null) continue;
                        if(projectiles.ContainsKey(projectile.Name))
                        {
                            projectiles[projectile.Name].Add(projectile);
                        }else
                        {
                            projectiles.Add(projectile.Name,new List<GameObject>());
                             projectiles[projectile.Name].Add(projectile);
                        }
                    }
                }
            }
            
            foreach(var projectileName in projectiles.Keys)
            {
                if(_gameObjects.ContainsKey(projectileName))
                {
                    foreach(var projectile in projectiles[projectileName])
                    {
                        _gameObjects[projectileName].Add(projectile);
                    }
                }else
                {
                    _gameObjects.Add(projectileName,projectiles[projectileName]);
                }
            }
        }
    }
}