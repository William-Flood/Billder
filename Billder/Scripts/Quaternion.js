function Quaternion(a, x, y, z) {
    this.x = x;
    this.y = y;
    this.z = z;
    this.a = a;

    this.hamilton = function (right) {
        return new Quaternion(
            this.a * right.a - this.x * right.x - this.y * right.y - this.z * right.z,
            this.a * right.x + this.x * right.a + this.y * right.z - this.z * right.y,
            this.a * right.y - this.x * right.z + this.y * right.a + this.z * right.x,
            this.a * right.z + this.x * right.y - this.y * right.x + this.z * right.a);
    }

    this.reciprocal = function()
    {

        return new Quaternion(
            this.a / this.lengthSquared(),
            -1 * this.x / this.lengthSquared(),
            -1 * this.y / this.lengthSquared(),
            -1 * this.z / this.lengthSquared()
            );
    }

    this.rotate = function (x, y, z) {
        var preRotateQuaternion = new Quaternion(0, x, y, z);
        var intermediate = this.hamilton(preRotateQuaternion);
        var rotatedQuaternion = intermediate.hamilton(this.reciprocal());
        return  {"X":rotatedQuaternion.x, "Y":rotatedQuaternion.y, "Z":rotatedQuaternion.z};

    }

    this.lengthSquared = function()
    {
        return this.a * this.a + this.x * this.x + this.y * this.y + this.z * this.z;
    }

    this.setRotationAngle = function (phi) {
        this.a = Math.cos(a * Math.PI / 360);
        var scale = 1;
        this.normalize();

    }

    this.normalize = function () {
        if (0 != (this.x * this.x + this.y * this.y + this.z * this.z)) {
            scale = Math.sqrt((1 - this.a * this.a) /
                (this.x * this.x + this.y * this.y + this.z * this.z));
            this.x = this.x * scale;
            this.y = this.y * scale;
            this.z = this.z * scale;
        }
    }

    this.normalize();
}
