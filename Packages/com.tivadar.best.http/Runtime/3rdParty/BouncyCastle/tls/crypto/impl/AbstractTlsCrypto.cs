#if !BESTHTTP_DISABLE_ALTERNATE_SSL && (!UNITY_WEBGL || UNITY_EDITOR)
#pragma warning disable
using System;
using System.Collections.Generic;

using Best.HTTP.SecureProtocol.Org.BouncyCastle.Math;
using Best.HTTP.SecureProtocol.Org.BouncyCastle.Security;
using Best.HTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace Best.HTTP.SecureProtocol.Org.BouncyCastle.Tls.Crypto.Impl
{
    /// <summary>Base class for a TlsCrypto implementation that provides some needed methods from elsewhere in the impl
    /// package.</summary>
    public abstract class AbstractTlsCrypto
        : TlsCrypto
    {
        public abstract bool HasAnyStreamVerifiers(IList<SignatureAndHashAlgorithm> signatureAndHashAlgorithms);

        public abstract bool HasAnyStreamVerifiersLegacy(short[] clientCertificateTypes);

        public abstract bool HasCryptoHashAlgorithm(int cryptoHashAlgorithm);

        public abstract bool HasCryptoSignatureAlgorithm(int cryptoSignatureAlgorithm);

        public abstract bool HasDHAgreement();

        public abstract bool HasECDHAgreement();

        public abstract bool HasEncryptionAlgorithm(int encryptionAlgorithm);

        public abstract bool HasHkdfAlgorithm(int cryptoHashAlgorithm);

        public abstract bool HasMacAlgorithm(int macAlgorithm);

        public abstract bool HasNamedGroup(int namedGroup);

        public abstract bool HasRsaEncryption();

        public abstract bool HasSignatureAlgorithm(short signatureAlgorithm);

        public abstract bool HasSignatureAndHashAlgorithm(SignatureAndHashAlgorithm sigAndHashAlgorithm);

        public abstract bool HasSignatureScheme(int signatureScheme);

        public abstract bool HasSrpAuthentication();

        public abstract TlsSecret CreateSecret(byte[] data);

        public abstract TlsSecret GenerateRsaPreMasterSecret(ProtocolVersion clientVersion);

        public abstract SecureRandom SecureRandom { get; }

        public virtual TlsCertificate CreateCertificate(byte[] encoding)
        {
            return CreateCertificate(CertificateType.X509, encoding);
        }

        public abstract TlsCertificate CreateCertificate(short type, byte[] encoding);

        public abstract TlsCipher CreateCipher(TlsCryptoParameters cryptoParams, int encryptionAlgorithm, int macAlgorithm);

        public abstract TlsDHDomain CreateDHDomain(TlsDHConfig dhConfig);

        public abstract TlsECDomain CreateECDomain(TlsECConfig ecConfig);

        public virtual TlsSecret AdoptSecret(TlsSecret secret)
        {
            // TODO[tls] Need an alternative that doesn't require AbstractTlsSecret (which holds literal data)
            if (secret is AbstractTlsSecret)
            {
                AbstractTlsSecret sec = (AbstractTlsSecret)secret;

                return CreateSecret(sec.CopyData());
            }

            throw new ArgumentException("unrecognized TlsSecret - cannot copy data: " + Org.BouncyCastle.Utilities.Platform.GetTypeName(secret));
        }

        public abstract TlsHash CreateHash(int cryptoHashAlgorithm);

        public abstract TlsHmac CreateHmac(int macAlgorithm);

        public abstract TlsHmac CreateHmacForHash(int cryptoHashAlgorithm);

        public abstract TlsNonceGenerator CreateNonceGenerator(byte[] additionalSeedMaterial);

#if NETCOREAPP2_1_OR_GREATER || NETSTANDARD2_1_OR_GREATER || UNITY_2021_2_OR_NEWER
        public abstract TlsNonceGenerator CreateNonceGenerator(ReadOnlySpan<byte> additionalSeedMaterial);
#endif

        public abstract TlsSrp6Client CreateSrp6Client(TlsSrpConfig srpConfig);

        public abstract TlsSrp6Server CreateSrp6Server(TlsSrpConfig srpConfig, BigInteger srpVerifier);

        public abstract TlsSrp6VerifierGenerator CreateSrp6VerifierGenerator(TlsSrpConfig srpConfig);

        public abstract TlsSecret HkdfInit(int cryptoHashAlgorithm);
    }
}
#pragma warning restore
#endif
