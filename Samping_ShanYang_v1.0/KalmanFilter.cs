namespace Samping_ShanYang_v1._0
{
    internal class KalmanFilter
    {
        private double _q = 0.0001; // 过程噪声
        private double _r = 0.1;    // 测量噪声
        private double _x = 0;      // 初始值
        private double _p = 1;      // 初始误差协方差
        private double _k = 0;      // 卡尔曼增益

        public double Update(double measurement)
        {
            // 预测更新
            _p = _p + _q;

            // 测量更新
            _k = _p / (_p + _r);
            _x = _x + _k * (measurement - _x);
            _p = (1 - _k) * _p;

            return _x;
        }
    }
}
