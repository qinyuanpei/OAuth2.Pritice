* 创建自签名文件和私钥文件

  ```shell
  makecert -a sha1  -sky exchange -n "CN=OAuth2.Pritice.Server" -b 05/30/2018 -e 01/01/2020 -sv C:\Users\PayneQin\Desktop\signserver.pvk C:\Users\PayneQin\Desktop\signserver.cer
  ```

* 利用证书.cer创建发行者证书.spc

  ```
  cert2spc C:\Users\PayneQin\Desktop\signserver.cer C:\Users\PayneQin\Desktop\signserver.spc
  ```

* 利用证书.pvk转换出.pfx文件

  ```
  pvk2pfx -pvk C:\Users\PayneQin\Desktop\signserver.pvk -pi 1234567890 -spc C:\Users\PayneQin\Desktop\signserver.spc
  ```

  ​