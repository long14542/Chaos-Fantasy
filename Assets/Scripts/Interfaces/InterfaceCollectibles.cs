// There will be a lot of different types of collectibles like health, money, exp, buffs
// It is easier to create an interface to define the behavior of the collectibles rather than a class
//because collectibles' behavior can vary based on what type of collectible that is
public interface InterfaceCollectibles
{
    void Collect();
}
