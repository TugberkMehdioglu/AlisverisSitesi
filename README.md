# AlisverisSitesi
 NTier architecture pattern ile yaptığım alışveriş sitesi simülasyonu, BankaAPI projem ile entegre çalışır.

Fakedata ile ürün ve kategoriler oluşturulmuştur, kullanıcı ürünleri sepete atabilir fakat satın almak için giriş yapmak zorundadır. Satın alma kısmında kullanıcıdan kart bilgileri istenir ve BankaAPI projesinden gelen successStatus ile satın alma gerçekleştirilir. Admin panelinde kategori ve ürün crud işlemleri bulunmaktadır, bu controller'lara giriş için adminAuthentication gereklidir. Üye olunduğunda verilen mail'e aktivasyon linki yollanır, bu linke tıklanıp user aktif edilmediği sürece oturum açılamaz. Kullanıcıların şifreleri DB'de kriptolanmış şekilde tutulur. EF CodeFirst kullanılmıştır.
