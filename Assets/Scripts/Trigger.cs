public class Trigger {
    private bool value;

    public void Set(bool input) {
        if (input) {
            value = true;
        }
    }

    public bool Consume() {
        if (value) {
            value = false;
            return true;
        }

        return false;
    }
}