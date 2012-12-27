namespace OclParser.typeChecker
{
    public class Messages {
        private static string BUNDLE_NAME = "br.ufrj.cos.lens.odyssey.tools.psw.typeChecker.Messages"; //$NON-NLS-1$
        private static ResourceBundle RESOURCE_BUNDLE = ResourceBundle.getBundle(BUNDLE_NAME);

        /**
     *
     */
        private Messages() {
            // TODO Auto-generated constructor stub
        }

        /**
     * @param key
     * @return
     */
        public static string getString(string key) {
            // TODO Auto-generated method stub
            try {
                return RESOURCE_BUNDLE.getString(key);
            } catch (MissingResourceException e) {
                return '!' + key + '!';
            }
        }
    }
}
