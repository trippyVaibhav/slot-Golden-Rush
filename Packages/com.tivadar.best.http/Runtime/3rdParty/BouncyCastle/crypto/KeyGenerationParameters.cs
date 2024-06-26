#if !BESTHTTP_DISABLE_ALTERNATE_SSL && (!UNITY_WEBGL || UNITY_EDITOR)
#pragma warning disable
using System;
using Best.HTTP.SecureProtocol.Org.BouncyCastle.Security;

namespace Best.HTTP.SecureProtocol.Org.BouncyCastle.Crypto
{
    /**
     * The base class for parameters to key generators.
     */
    public class KeyGenerationParameters
    {
        private SecureRandom	random;
        private int				strength;

        /**
         * initialise the generator with a source of randomness
         * and a strength (in bits).
         *
         * @param random the random byte source.
         * @param strength the size, in bits, of the keys we want to produce.
         */
        public KeyGenerationParameters(
            SecureRandom	random,
            int				strength)
        {
			if (random == null)
				throw new ArgumentNullException("random");
			if (strength < 1)
				throw new ArgumentException("strength must be a positive value", "strength");

			this.random = random;
            this.strength = strength;
        }

		/**
         * return the random source associated with this
         * generator.
         *
         * @return the generators random source.
         */
        public SecureRandom Random
        {
            get { return random; }
        }

		/**
         * return the bit strength for keys produced by this generator,
         *
         * @return the strength of the keys this generator produces (in bits).
         */
        public int Strength
        {
            get { return strength; }
        }
    }
}
#pragma warning restore
#endif
